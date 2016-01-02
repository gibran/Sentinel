using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Sentinel.Api.Filters
{
    internal class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            if (actionExecutedContext?.Response == null || !actionExecutedContext.Response.IsSuccessStatusCode) return;
            var cc = new CacheControlHeaderValue
            {
                NoStore = true,
                NoCache = true,
                Private = true,
                MaxAge = TimeSpan.Zero
            };
            actionExecutedContext.Response.Headers.CacheControl = cc;

            actionExecutedContext.Response.Headers.Add("Pragma", "no-cache");
        }
    }
}