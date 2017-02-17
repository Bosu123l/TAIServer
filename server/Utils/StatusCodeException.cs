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
    public class StatusCodeExceptionAttribute : ExceptionFilterAttribute
    {
        private void HandleException(ExceptionContext context)
        {
            if(context.Exception is StatusCodeException)
            {
                var statusException = (StatusCodeException)context.Exception;
                var result = new ContentResult() {
                    ContentType = "application/json",
                    StatusCode = statusException.StatusCode,
                    Content = JsonConvert.SerializeObject(new  { Message = statusException.StatusMessage })
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

    public class StatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public StatusCodeException(int statusCode, string statusMessage = "")
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }
    }
}
