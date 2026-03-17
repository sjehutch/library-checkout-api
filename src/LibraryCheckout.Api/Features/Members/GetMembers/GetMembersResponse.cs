namespace LibraryCheckout.Api.Features.Members.GetMembers;

public sealed record GetMembersResponse(
    Guid Id,
    string FullName,
    string Email);
