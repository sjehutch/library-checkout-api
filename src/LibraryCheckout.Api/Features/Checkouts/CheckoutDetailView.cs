namespace LibraryCheckout.Api.Features.Checkouts;

public sealed record CheckoutDetailView(
    Guid Id,
    Guid BookId,
    string BookTitle,
    Guid MemberId,
    string MemberName,
    DateTimeOffset CheckedOutAtUtc,
    DateTimeOffset DueAtUtc,
    DateTimeOffset? ReturnedAtUtc,
    bool IsOverdue);
