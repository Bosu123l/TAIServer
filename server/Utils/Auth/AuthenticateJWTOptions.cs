using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace TAI.Utils.Auth
{
    public class AuthenticateJWTOptions : AuthenticationOptions, IOptions<AuthenticateJWTOptions>
    {
        public AuthenticateJWTOptions()
        {
            AuthenticationScheme = "JWT";
            AutomaticAuthenticate = true;
            AutomaticChallenge = true;
        }

        public AuthenticateJWTOptions Value
        {
            get
            {
                return this;
            }
        }
    }
}
