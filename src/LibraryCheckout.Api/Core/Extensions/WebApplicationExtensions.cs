using LibraryCheckout.Api.Core.Security;
using LibraryCheckout.Api.Features.Books.GetBooks;
using LibraryCheckout.Api.Features.Checkouts.CheckoutBook;
using LibraryCheckout.Api.Features.Checkouts.GetCheckouts;
using LibraryCheckout.Api.Features.Checkouts.GetOverdueCheckouts;
using LibraryCheckout.Api.Features.Checkouts.ReturnBook;
using LibraryCheckout.Api.Features.Members.GetMembers;

namespace LibraryCheckout.Api.Core.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApiPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            /*
             * Swagger stays open on purpose for this take-home.
             * Reviewers should be able to start the API and test it immediately.
             * In a production system, access to the API surface would be protected,
             * but adding that friction here would make the project harder to review.
             */
            app.MapGet("/", () => Results.Redirect("/swagger/index.html", permanent: false))
                .ExcludeFromDescription();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();
        app.UseExceptionHandler();
        app.UseSecurityHeaders();

        /*
         * HTTPS redirection is useful in real deployments, but this take-home also
         * supports plain HTTP so reviewers can run Swagger locally without dealing
         * with certificate trust issues. Production would keep HTTPS enforced.
         */
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();

        return app;
    }

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGetBooks();
        endpoints.MapGetMembers();
        endpoints.MapGetCheckouts();
        endpoints.MapGetOverdueCheckouts();
        endpoints.MapCheckoutBook();
        endpoints.MapReturnBook();

        return endpoints;
    }
}
