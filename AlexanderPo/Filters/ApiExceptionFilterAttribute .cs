using System.Web.Http.Filters;
using AlexanderPo.Loggers;
using Any.Logs;

namespace AlexanderPo.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log.Out.Error(actionExecutedContext.Exception, "Api Exception");
            base.OnException(actionExecutedContext);
        }
    }
}