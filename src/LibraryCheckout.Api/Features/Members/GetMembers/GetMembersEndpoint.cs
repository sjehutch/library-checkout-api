namespace LibraryCheckout.Api.Features.Members.GetMembers;

public static class GetMembersEndpoint
{
    public static IEndpointRouteBuilder MapGetMembers(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/members", (MembersService service) =>
            {
                var response = service.GetMembers()
                    .Select(member => new GetMembersResponse(
                        member.Id,
                        member.FullName,
                        member.Email));

                return TypedResults.Ok(response);
            })
            .WithName("GetMembers")
            .WithSummary("Get all members")
            .WithDescription("Returns the sample library members that can be used when testing checkout flows in Swagger.")
            .Produces<IEnumerable<GetMembersResponse>>(StatusCodes.Status200OK);

        return endpoints;
    }
}
