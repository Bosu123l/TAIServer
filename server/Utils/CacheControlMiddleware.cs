using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAI.Utils
{
    public class CacheControlMiddleware
    {
        private readonly RequestDelegate _next;

        public CacheControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var httpContext = state as HttpContext;
                httpContext.Response.Headers.Add("Cache-Control", new[] { "no-cache" });
                return Task.FromResult(0);
            }, context);

            await _next(context);
        }
    }
}
