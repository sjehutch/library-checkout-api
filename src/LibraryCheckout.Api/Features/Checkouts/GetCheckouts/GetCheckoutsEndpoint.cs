namespace LibraryCheckout.Api.Features.Checkouts.GetCheckouts;

public static class GetCheckoutsEndpoint
{
    public static IEndpointRouteBuilder MapGetCheckouts(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/checkouts", (CheckoutService service) =>
            {
                var response = service.GetCheckouts()
                    .Select(checkout => checkout.ToResponse());

                return TypedResults.Ok(response);
            })
            .WithName("GetCheckouts")
            .WithSummary("Get all checkouts")
            .WithDescription("Returns every checkout in the in-memory store, including active, overdue, and returned records.")
            .Produces<IEnumerable<CheckoutResponse>>(StatusCodes.Status200OK);

        return endpoints;
    }
}
