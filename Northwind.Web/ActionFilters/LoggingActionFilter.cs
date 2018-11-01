using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Northwind.Web.ActionFilters
{
    public class LoggingActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ILogger logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ILogger logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger));
        }
    }
}
