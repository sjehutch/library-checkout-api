namespace LibraryCheckout.Api.Data.Seed;

public static class LibraryDataSeeder
{
    public static InMemoryLibraryStore CreateStore()
    {
        var nowUtc = DateTimeOffset.UtcNow;

        return new InMemoryLibraryStore(
            LibrarySeedData.CreateBooks(),
            LibrarySeedData.CreateMembers(),
            LibrarySeedData.CreateCheckouts(nowUtc));
    }
}
