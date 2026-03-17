namespace LibraryCheckout.Api.Features.Books.GetBooks;

public static class GetBooksEndpoint
{
    public static IEndpointRouteBuilder MapGetBooks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/books", (BooksService service) =>
            {
                var response = service.GetBooks()
                    .Select(book => new GetBooksResponse(
                        book.Id,
                        book.Title,
                        book.Author,
                        book.IsAvailable));

                return TypedResults.Ok(response);
            })
            .WithName("GetBooks")
            .WithSummary("Get all books")
            .WithDescription("Returns the seeded library collection and shows whether each book is currently available for checkout.")
            .Produces<IEnumerable<GetBooksResponse>>(StatusCodes.Status200OK);

        return endpoints;
    }
}
