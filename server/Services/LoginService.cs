using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TAI.Services;
using TAI.Utils;
using TAI.Utils.Helpers;
using TAIServer.Models;
using TAIServer.Entities.DataAccess;
using TAIServer.Services.Interfaces;

namespace TAIServer.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(DataContext context) : base(context)
        {
        }

        public UserModel Login(LoginModel model)
        {
            if (String.IsNullOrWhiteSpace(model.User) || String.IsNullOrWhiteSpace(model.Password))
                throw new StatusCodeException((int)HttpStatusCode.NotAcceptable);

            var user = DataContext.Members.FirstOrDefault(m => m.Login == model.User && m.Password == Common.Sha256(model.Password));

            if (user == null)
                throw new StatusCodeException((int)HttpStatusCode.BadRequest);

            var payload = new Dictionary<string, object>()
            {
                { "login", user.Login }
            };

            var token = JWTHelper.EncodeToken(payload);

            var userModel = new UserModel()
            {
                Id = (int)user.Id,
                Login = user.Login,
                Token = token
            };
            return userModel;
        }

        public void Logout()
        {
            
        }
    }
}
