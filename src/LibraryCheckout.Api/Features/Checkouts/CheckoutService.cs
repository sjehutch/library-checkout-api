using LibraryCheckout.Api.Data;

namespace LibraryCheckout.Api.Features.Checkouts;

public sealed class CheckoutService(InMemoryLibraryStore store, ILogger<CheckoutService> logger)
{
    private static readonly TimeSpan CheckoutLength = TimeSpan.FromDays(14);

    public IEnumerable<CheckoutDetailView> GetCheckouts()
    {
        var nowUtc = DateTimeOffset.UtcNow;

        return store.Checkouts
            .OrderByDescending(checkout => checkout.CheckedOutAtUtc)
            .Select(checkout => CreateView(checkout, nowUtc));
    }

    public IEnumerable<CheckoutDetailView> GetOverdueCheckouts()
    {
        var nowUtc = DateTimeOffset.UtcNow;

        return store.Checkouts
            .Where(checkout => checkout.IsOverdue(nowUtc))
            .OrderBy(checkout => checkout.DueAtUtc)
            .Select(checkout => CreateView(checkout, nowUtc));
    }

    public CheckoutOperationResult CheckoutBook(Guid bookId, Guid memberId)
    {
        return store.ExecuteLocked(() =>
        {
            var book = store.Books.SingleOrDefault(candidate => candidate.Id == bookId);
            if (book is null)
            {
                return CheckoutOperationResult.Failure("book_not_found", "The requested book was not found.");
            }

            var member = store.Members.SingleOrDefault(candidate => candidate.Id == memberId);
            if (member is null)
            {
                return CheckoutOperationResult.Failure("member_not_found", "The requested member was not found.");
            }

            var existingCheckout = store.Checkouts.SingleOrDefault(checkout => checkout.BookId == bookId && !checkout.IsReturned);
            if (existingCheckout is not null)
            {
                logger.LogInformation(
                    "Checkout blocked because book {BookId} is already checked out with checkout {CheckoutId}",
                    bookId,
                    existingCheckout.Id);

                return CheckoutOperationResult.Failure("book_unavailable", "This book is already checked out.");
            }

            var checkedOutAtUtc = DateTimeOffset.UtcNow;
            var checkout = new Checkout
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                MemberId = memberId,
                CheckedOutAtUtc = checkedOutAtUtc,
                DueAtUtc = checkedOutAtUtc.Add(CheckoutLength)
            };

            store.Checkouts.Add(checkout);

            logger.LogInformation(
                "Book {BookId} checked out to member {MemberId} with checkout {CheckoutId}",
                bookId,
                memberId,
                checkout.Id);

            return CheckoutOperationResult.Success(CreateView(checkout, checkedOutAtUtc));
        });
    }

    public CheckoutOperationResult ReturnBook(Guid checkoutId)
    {
        return store.ExecuteLocked(() =>
        {
            var checkout = store.Checkouts.SingleOrDefault(candidate => candidate.Id == checkoutId);
            if (checkout is null)
            {
                return CheckoutOperationResult.Failure("checkout_not_found", "The requested checkout was not found.");
            }

            if (checkout.IsReturned)
            {
                return CheckoutOperationResult.Failure("checkout_already_returned", "This checkout has already been returned.");
            }

            var returnedAtUtc = DateTimeOffset.UtcNow;
            checkout.MarkReturned(returnedAtUtc);

            logger.LogInformation(
                "Checkout {CheckoutId} returned for book {BookId} by member {MemberId}",
                checkout.Id,
                checkout.BookId,
                checkout.MemberId);

            return CheckoutOperationResult.Success(CreateView(checkout, returnedAtUtc));
        });
    }

    private CheckoutDetailView CreateView(Checkout checkout, DateTimeOffset nowUtc)
    {
        var book = store.Books.Single(book => book.Id == checkout.BookId);
        var member = store.Members.Single(member => member.Id == checkout.MemberId);

        return new CheckoutDetailView(
            checkout.Id,
            book.Id,
            book.Title,
            member.Id,
            member.FullName,
            checkout.CheckedOutAtUtc,
            checkout.DueAtUtc,
            checkout.ReturnedAtUtc,
            checkout.IsOverdue(nowUtc));
    }
}
