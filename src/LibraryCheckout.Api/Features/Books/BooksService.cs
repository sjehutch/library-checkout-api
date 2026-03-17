using LibraryCheckout.Api.Data;

namespace LibraryCheckout.Api.Features.Books;

public sealed class BooksService(InMemoryLibraryStore store)
{
    public IEnumerable<BookAvailabilityView> GetBooks()
    {
        var activeBookIds = store.Checkouts
            .Where(checkout => !checkout.IsReturned)
            .Select(checkout => checkout.BookId)
            .ToHashSet();

        return store.Books
            .OrderBy(book => book.Title)
            .Select(book => new BookAvailabilityView(
                book.Id,
                book.Title,
                book.Author,
                !activeBookIds.Contains(book.Id)));
    }
}
