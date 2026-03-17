using LibraryCheckout.Api.Core.Errors;
using LibraryCheckout.Api.Features.Checkouts.GetCheckouts;

namespace LibraryCheckout.Api.Features.Checkouts.ReturnBook;

public static class ReturnBookEndpoint
{
    public static IEndpointRouteBuilder MapReturnBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/checkouts/{checkoutId:guid}/return", (
                Guid checkoutId,
                CheckoutService service,
                HttpContext context) =>
            {
                if (checkoutId == Guid.Empty)
                {
                    return ApiResults.BadRequest("validation_error", "CheckoutId is required.", context);
                }

                var result = service.ReturnBook(checkoutId);
                if (!result.IsSuccess)
                {
                    return result.ErrorCode switch
                    {
                        "checkout_not_found" => ApiResults.NotFound(result.ErrorCode, result.ErrorMessage!, context),
                        "checkout_already_returned" => ApiResults.Conflict(result.ErrorCode, result.ErrorMessage!, context),
                        _ => ApiResults.BadRequest(result.ErrorCode!, result.ErrorMessage!, context)
                    };
                }

                var response = result.Checkout!.ToResponse();
                return ApiResults.Ok(response);
            })
            .WithName("ReturnBook")
            .WithSummary("Return a checked out book")
            .WithDescription("Marks an active checkout as returned so the related book becomes available again.")
            .Produces<CheckoutResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
            .Produces<ErrorResponse>(StatusCodes.Status409Conflict);

        return endpoints;
    }
}
