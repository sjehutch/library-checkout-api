namespace LibraryCheckout.Api.Features.Books;

public sealed record BookAvailabilityView(
    Guid Id,
    string Title,
    string Author,
    bool IsAvailable);
