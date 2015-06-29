using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace AlexanderPo.Filters
{
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true
                };
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}