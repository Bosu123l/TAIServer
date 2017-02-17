using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TAIServer.Entities;
using TAIServer.Services.Interfaces;

namespace TAIServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MemberController
    {
        private readonly IMembersService _members;

        public MemberController(IMembersService members)
        {
            _members = members;
        }

        [HttpGet("{id}")]
        public Member GetMember(long? id)
        {
            return _members.GetMember(id.Value);
        }

        [HttpPost]
        public Member AddMember(Member member)
        {
            return _members.AddMember(member);
        }

        [HttpDelete("{id}")]
        public Member RemoveMember(long? id)
        {
            return _members.RemoveMember(id.Value);
        }

        [HttpPut]
        public Member UpdateMember([FromBody]Member member)
        {
            return _members.UpdateMember(member);
        }

        [HttpGet]
        public IEnumerable<Member> GetMembers()
        {
            return _members.GetMembers();
        }
    }
}