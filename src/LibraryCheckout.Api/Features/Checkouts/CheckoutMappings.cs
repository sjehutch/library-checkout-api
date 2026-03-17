using LibraryCheckout.Api.Features.Checkouts.GetCheckouts;

namespace LibraryCheckout.Api.Features.Checkouts;

public static class CheckoutMappings
{
    public static CheckoutResponse ToResponse(this CheckoutDetailView checkout)
    {
        return new CheckoutResponse(
            checkout.Id,
            checkout.BookId,
            checkout.BookTitle,
            checkout.MemberId,
            checkout.MemberName,
            checkout.CheckedOutAtUtc,
            checkout.DueAtUtc,
            checkout.ReturnedAtUtc,
            checkout.IsOverdue);
    }
}
