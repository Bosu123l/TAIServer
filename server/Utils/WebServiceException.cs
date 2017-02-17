using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace TAI.Utils
{
    public class WebServiceExceptionAttribute : ExceptionFilterAttribute
    {
        private void HandleException(ExceptionContext context)
        {
            if (context.Exception is CommunicationException)
            {
                var result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.ServiceUnavailable,
                    Content = JsonConvert.SerializeObject(new { Message = "" })
                };

                context.Result = result;
            }
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            HandleException(context);
            return base.OnExceptionAsync(context);
        }
    }
}
