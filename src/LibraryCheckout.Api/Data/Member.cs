namespace LibraryCheckout.Api.Data;

public sealed class Member
{
    public Guid Id { get; init; }

    public required string FullName { get; init; }

    public required string Email { get; init; }
}
