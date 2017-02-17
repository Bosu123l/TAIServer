using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TAI.Utils.Auth
{
    public class AuthenticateJWTMiddleware : AuthenticationMiddleware<AuthenticateJWTOptions>
    {
        public AuthenticateJWTMiddleware(RequestDelegate next, AuthenticateJWTOptions authenticationOptions, ILoggerFactory loggerFactory)
            : base(next, authenticationOptions, loggerFactory, System.Text.Encodings.Web.UrlEncoder.Create())
        {
        }

        protected override AuthenticationHandler<AuthenticateJWTOptions> CreateHandler()
        {
            return new AuthenticateJWTHandler();
        }
    }
}
