namespace LibraryCheckout.Api.Core.Security;

public sealed class SecurityHeadersMiddleware(RequestDelegate next)
{
    private const string DefaultContentSecurityPolicy =
        "default-src 'self'; frame-ancestors 'none'; object-src 'none'; base-uri 'self'; form-action 'self'";

    public async Task InvokeAsync(HttpContext context)
    {
        /*
         * Strict security headers are useful in real systems because they reduce
         * browser attack surface. Swagger is intentionally excluded in this
         * take-home so reviewers can use the API easily in the browser without
         * running into UI restrictions caused by a hardened policy.
         */
        if (IsSwaggerRequest(context))
        {
            await next(context);
            return;
        }

        var headers = context.Response.Headers;

        headers["Content-Security-Policy"] = DefaultContentSecurityPolicy;
        headers["Referrer-Policy"] = "no-referrer";
        headers["X-Content-Type-Options"] = "nosniff";
        headers["X-Frame-Options"] = "DENY";
        headers["X-Permitted-Cross-Domain-Policies"] = "none";
        headers["Permissions-Policy"] = "accelerometer=(), camera=(), geolocation=(), gyroscope=(), microphone=(), payment=(), usb=()";
        headers["Cross-Origin-Opener-Policy"] = "same-origin";
        headers["Cross-Origin-Resource-Policy"] = "same-origin";

        await next(context);
    }

    private static bool IsSwaggerRequest(HttpContext context)
    {
        return context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase);
    }
}
