namespace LibraryCheckout.Api.Core.Errors;

public sealed record ErrorResponse(
    string Code,
    string Message,
    int Status,
    string? TraceId = null);
