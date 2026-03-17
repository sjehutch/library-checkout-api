using LibraryCheckout.Api.Core.Security;

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
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();
        app.UseExceptionHandler();
        app.UseSecurityHeaders();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        return app;
    }

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}
