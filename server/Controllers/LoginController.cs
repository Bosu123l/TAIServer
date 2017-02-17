using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAIServer.Models;
using TAIServer.Services.Interfaces;

namespace TAIServer.Controllers
{

    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public UserModel Post([FromBody]LoginModel model)
        {
            return _loginService.Login(model);
        }
    }
}