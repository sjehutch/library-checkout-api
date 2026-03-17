namespace LibraryCheckout.Api.Features.Checkouts.GetCheckouts;

public sealed record CheckoutResponse(
    Guid Id,
    Guid BookId,
    string BookTitle,
    Guid MemberId,
    string MemberName,
    DateTimeOffset CheckedOutAtUtc,
    DateTimeOffset DueAtUtc,
    DateTimeOffset? ReturnedAtUtc,
    bool IsOverdue);
