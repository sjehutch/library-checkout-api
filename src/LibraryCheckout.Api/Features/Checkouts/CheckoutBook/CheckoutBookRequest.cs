namespace LibraryCheckout.Api.Features.Checkouts.CheckoutBook;

public sealed record CheckoutBookRequest(
    Guid BookId,
    Guid MemberId);
