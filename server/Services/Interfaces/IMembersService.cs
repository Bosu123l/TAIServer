using System.Collections.Generic;
using TAIServer.Entities;

namespace TAIServer.Services.Interfaces
{
    public interface IMembersService
    {
        Member GetMember(long id);
        Member UpdateMember(Member member);

        Member RemoveMember(long id);

        Member AddMember(Member member);

        IEnumerable<Member> GetMembers();
        bool RegisterMember(Member member);
    }
}