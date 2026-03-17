using LibraryCheckout.Api.Core.Errors;
using LibraryCheckout.Api.Features.Checkouts.GetCheckouts;

namespace LibraryCheckout.Api.Features.Checkouts.CheckoutBook;

public static class CheckoutBookEndpoint
{
    public static IEndpointRouteBuilder MapCheckoutBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/checkouts", async (
                CheckoutBookRequest request,
                IValidator<CheckoutBookRequest> validator,
                CheckoutService service,
                HttpContext context) =>
            {
                var validationResult = await validator.ValidateAsync(request, context.RequestAborted);
                if (!validationResult.IsValid)
                {
                    var message = string.Join(" ", validationResult.Errors.Select(error => error.ErrorMessage));
                    return ApiResults.BadRequest("validation_error", message, context);
                }

                var result = service.CheckoutBook(request.BookId, request.MemberId);
                if (!result.IsSuccess)
                {
                    return result.ErrorCode switch
                    {
                        "book_not_found" => ApiResults.NotFound(result.ErrorCode, result.ErrorMessage!, context),
                        "member_not_found" => ApiResults.NotFound(result.ErrorCode, result.ErrorMessage!, context),
                        "book_unavailable" => ApiResults.Conflict(result.ErrorCode, result.ErrorMessage!, context),
                        _ => ApiResults.BadRequest(result.ErrorCode!, result.ErrorMessage!, context)
                    };
                }

                var response = result.Checkout!.ToResponse();
                return ApiResults.Created($"/api/checkouts/{response.Id}", response);
            })
            .WithName("CheckoutBook")
            .WithSummary("Check out a book")
            .WithDescription("Creates a new checkout when the book exists, the member exists, and the book is currently available.")
            .Produces<CheckoutResponse>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
            .Produces<ErrorResponse>(StatusCodes.Status409Conflict);

        return endpoints;
    }
}
