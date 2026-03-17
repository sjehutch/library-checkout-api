namespace LibraryCheckout.Api.Data;

public sealed class Checkout
{
    public Guid Id { get; init; }

    public Guid BookId { get; init; }

    public Guid MemberId { get; init; }

    public DateTimeOffset CheckedOutAtUtc { get; init; }

    public DateTimeOffset DueAtUtc { get; init; }

    public DateTimeOffset? ReturnedAtUtc { get; private set; }

    public bool IsReturned => ReturnedAtUtc is not null;

    public bool IsOverdue(DateTimeOffset nowUtc)
    {
        return ReturnedAtUtc is null && DueAtUtc < nowUtc;
    }

    public void MarkReturned(DateTimeOffset returnedAtUtc)
    {
        ReturnedAtUtc = returnedAtUtc;
    }
}
