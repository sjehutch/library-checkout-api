namespace LibraryCheckout.Api.Data;

public sealed class Book
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public required string Author { get; init; }
}
