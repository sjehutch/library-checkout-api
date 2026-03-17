namespace LibraryCheckout.Api.Data.Seed;

public static class LibrarySeedData
{
    public static readonly Guid CleanCodeBookId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    public static readonly Guid PragmaticProgrammerBookId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    public static readonly Guid DomainDrivenDesignBookId = Guid.Parse("10000000-0000-0000-0000-000000000003");
    public static readonly Guid RefactoringBookId = Guid.Parse("10000000-0000-0000-0000-000000000004");

    public static readonly Guid AvaMemberId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    public static readonly Guid MateoMemberId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    public static readonly Guid PriyaMemberId = Guid.Parse("20000000-0000-0000-0000-000000000003");

    public static IReadOnlyList<Book> CreateBooks()
    {
        return
        [
            new Book { Id = CleanCodeBookId, Title = "Clean Code", Author = "Robert C. Martin" },
            new Book { Id = PragmaticProgrammerBookId, Title = "The Pragmatic Programmer", Author = "Andrew Hunt and David Thomas" },
            new Book { Id = DomainDrivenDesignBookId, Title = "Domain-Driven Design", Author = "Eric Evans" },
            new Book { Id = RefactoringBookId, Title = "Refactoring", Author = "Martin Fowler" }
        ];
    }

    public static IReadOnlyList<Member> CreateMembers()
    {
        return
        [
            new Member { Id = AvaMemberId, FullName = "Ava Thompson", Email = "ava.thompson@example.com" },
            new Member { Id = MateoMemberId, FullName = "Mateo Rivera", Email = "mateo.rivera@example.com" },
            new Member { Id = PriyaMemberId, FullName = "Priya Patel", Email = "priya.patel@example.com" }
        ];
    }

    public static IReadOnlyList<Checkout> CreateCheckouts(DateTimeOffset nowUtc)
    {
        return
        [
            new Checkout
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                BookId = PragmaticProgrammerBookId,
                MemberId = AvaMemberId,
                CheckedOutAtUtc = nowUtc.AddDays(-5),
                DueAtUtc = nowUtc.AddDays(9)
            },
            new Checkout
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                BookId = DomainDrivenDesignBookId,
                MemberId = MateoMemberId,
                CheckedOutAtUtc = nowUtc.AddDays(-20),
                DueAtUtc = nowUtc.AddDays(-6)
            }
        ];
    }
}
