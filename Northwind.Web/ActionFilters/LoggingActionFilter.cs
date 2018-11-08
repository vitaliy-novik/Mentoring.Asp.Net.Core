using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Northwind.Web.ActionFilters
{
    public class LoggingActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ILogger logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger<LoggingActionFilter>));
			logger.LogInformation(
				string.Format($"{context.HttpContext.TraceIdentifier}: {context.ActionDescriptor.DisplayName} STARTED at {DateTime.Now}"));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ILogger logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger<LoggingActionFilter>));
			logger.LogInformation(
				string.Format($"{context.HttpContext.TraceIdentifier}: {context.ActionDescriptor.DisplayName} ENDED AT {DateTime.Now}"));
		}
    }
}
