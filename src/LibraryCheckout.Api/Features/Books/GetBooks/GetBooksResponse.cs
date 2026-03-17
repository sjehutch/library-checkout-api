namespace LibraryCheckout.Api.Features.Books.GetBooks;

public sealed record GetBooksResponse(
    Guid Id,
    string Title,
    string Author,
    bool IsAvailable);
