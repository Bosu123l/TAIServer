using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAIServer.Models;

namespace TAIServer.Services.Interfaces
{
    public interface ILoginService
    {
        UserModel Login(LoginModel model);
        void Logout();
    }
}
