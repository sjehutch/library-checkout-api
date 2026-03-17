namespace LibraryCheckout.Api.Features.Checkouts;

public sealed record CheckoutOperationResult(
    CheckoutDetailView? Checkout,
    string? ErrorCode,
    string? ErrorMessage)
{
    public bool IsSuccess => Checkout is not null;

    public static CheckoutOperationResult Success(CheckoutDetailView checkout)
    {
        return new CheckoutOperationResult(checkout, null, null);
    }

    public static CheckoutOperationResult Failure(string code, string message)
    {
        return new CheckoutOperationResult(null, code, message);
    }
}
