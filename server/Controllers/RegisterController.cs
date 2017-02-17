using Microsoft.AspNetCore.Mvc;
using TAIServer.Entities;
using TAIServer.Services.Interfaces;

namespace TAIServer.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly IMembersService _membersService;

        public RegisterController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [HttpPost]
        public bool RegisterMember([FromBody]Member member)
        {
            if (member == null)
                return false;
            return _membersService.RegisterMember(member);
        }
    }
}