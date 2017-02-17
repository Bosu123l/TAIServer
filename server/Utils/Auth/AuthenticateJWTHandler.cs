using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using TAI.Utils.Helpers;

namespace TAI.Utils.Auth
{
    public class ApplicationUser : IIdentity
    {
        public ApplicationUser(string authenticationType, bool isAuthenticated, string name)
        {
            this.AuthenticationType = authenticationType;
            this.IsAuthenticated = isAuthenticated;
            this.Name = name;
        }

        public string AuthenticationType
        {
            get;
        }

        public bool IsAuthenticated
        {
            get;
        }

        public string Name
        {
            get;
        }
    }

    public class AuthenticateJWTHandler : AuthenticationHandler<AuthenticateJWTOptions>
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenStrings = Request.Headers["JWT"];
            if (tokenStrings.Any())
            {
                try
                {
                    var payload = JWTHelper.DecodeToken(tokenStrings[0]);
                    var claimsPrincipal = new ClaimsPrincipal(new ApplicationUser(Options.AuthenticationScheme, true, payload["login"].ToString()));
                    var currentIdentity = claimsPrincipal.Identities.First();
                    var authenticationProperties = new AuthenticationProperties();
                    var authenticationTicket = new AuthenticationTicket(claimsPrincipal, authenticationProperties, this.Options.AuthenticationScheme);
                    var result = AuthenticateResult.Success(authenticationTicket);

                    return Task.FromResult(result);
                }
                catch (Exception)
                {
                    var result = AuthenticateResult.Fail("Invalid JWT token");
                    return Task.FromResult(result);
                }
            }

            return Task.FromResult(AuthenticateResult.Fail("No JWT token provided"));
        }
    }
}
