using LibraryCheckout.Api.Features.Checkouts.GetCheckouts;

namespace LibraryCheckout.Api.Features.Checkouts.GetOverdueCheckouts;

public static class GetOverdueCheckoutsEndpoint
{
    public static IEndpointRouteBuilder MapGetOverdueCheckouts(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/checkouts/overdue", (CheckoutService service) =>
            {
                var response = service.GetOverdueCheckouts()
                    .Select(checkout => checkout.ToResponse());

                return TypedResults.Ok(response);
            })
            .WithName("GetOverdueCheckouts")
            .WithSummary("Get overdue checkouts")
            .WithDescription("Returns only active checkouts whose due date has already passed.")
            .Produces<IEnumerable<CheckoutResponse>>(StatusCodes.Status200OK);

        return endpoints;
    }
}
