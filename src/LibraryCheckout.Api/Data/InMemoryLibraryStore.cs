namespace LibraryCheckout.Api.Data;

public sealed class InMemoryLibraryStore
{
    private readonly object _lock = new();

    public InMemoryLibraryStore(IEnumerable<Book> books, IEnumerable<Member> members, IEnumerable<Checkout> checkouts)
    {
        Books = books.ToList();
        Members = members.ToList();
        Checkouts = checkouts.ToList();
    }

    public List<Book> Books { get; }

    public List<Member> Members { get; }

    public List<Checkout> Checkouts { get; }

    public T ExecuteLocked<T>(Func<T> action)
    {
        lock (_lock)
        {
            return action();
        }
    }
}
