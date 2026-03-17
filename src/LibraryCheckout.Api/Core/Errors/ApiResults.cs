namespace LibraryCheckout.Api.Core.Errors;

public static class ApiResults
{
    public static IResult Ok<T>(T response)
    {
        return Results.Ok(response);
    }

    public static IResult Created<T>(string location, T response)
    {
        return Results.Created(location, response);
    }

    public static IResult BadRequest(string code, string message, HttpContext context)
    {
        return CreateErrorResult(HttpStatusCode.BadRequest, code, message, context);
    }

    public static IResult NotFound(string code, string message, HttpContext context)
    {
        return CreateErrorResult(HttpStatusCode.NotFound, code, message, context);
    }

    public static IResult Conflict(string code, string message, HttpContext context)
    {
        return CreateErrorResult(HttpStatusCode.Conflict, code, message, context);
    }

    private static IResult CreateErrorResult(
        HttpStatusCode statusCode,
        string code,
        string message,
        HttpContext context)
    {
        var response = new ErrorResponse(
            code,
            message,
            (int)statusCode,
            context.TraceIdentifier);

        return Results.Json(response, statusCode: (int)statusCode);
    }
}
