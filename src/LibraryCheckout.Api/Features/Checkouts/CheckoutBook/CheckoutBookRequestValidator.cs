namespace LibraryCheckout.Api.Features.Checkouts.CheckoutBook;

public sealed class CheckoutBookRequestValidator : AbstractValidator<CheckoutBookRequest>
{
    public CheckoutBookRequestValidator()
    {
        RuleFor(request => request.BookId)
            .NotEmpty()
            .WithMessage("BookId is required.");

        RuleFor(request => request.MemberId)
            .NotEmpty()
            .WithMessage("MemberId is required.");
    }
}
