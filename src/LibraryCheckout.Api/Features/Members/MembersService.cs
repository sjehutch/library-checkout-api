using LibraryCheckout.Api.Data;

namespace LibraryCheckout.Api.Features.Members;

public sealed class MembersService(InMemoryLibraryStore store)
{
    public IEnumerable<Member> GetMembers()
    {
        return store.Members.OrderBy(member => member.FullName);
    }
}
