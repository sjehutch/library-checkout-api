using System.Reflection;
using Microsoft.OpenApi.Models;

namespace LibraryCheckout.Api.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            /*
             * Swagger is intentionally left open in this take-home project.
             * The goal is to let reviewers run the API locally, open the UI,
             * and exercise endpoints without first dealing with tokens or login flows.
             * In a real system, the API itself would still be protected and Swagger
             * access would be reviewed as part of the broader security design.
             */
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Library Checkout API",
                Version = "v1",
                Description = "Core infrastructure for the Library Checkout API."
            });
        });

        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
    {
        /*
         * In production, this project would enforce a fallback authorization policy
         * so every endpoint is protected by default unless it is explicitly opened.
         * For this take-home, authentication is intentionally not enforced.
         * That is a deliberate choice so reviewers can test endpoints easily in Swagger
         * without needing tokens, local identity setup, or extra onboarding steps.
         */
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddApiValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
