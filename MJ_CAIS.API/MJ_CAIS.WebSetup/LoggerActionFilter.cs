using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.WebSetup
{
    public class LoggerActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LoggerActionFilter> _logger;

        public LoggerActionFilter(ILogger<LoggerActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context?.ActionDescriptor is ControllerActionDescriptor)
            {
                var controllerDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var controllerName = controllerDescriptor.ControllerName;
                var actionName = controllerDescriptor.ActionName;
                var identityName = context?.HttpContext?.User?.Identity?.Name;
                var path = context?.HttpContext?.Request?.Path;
                var queryString = context?.HttpContext?.Request?.QueryString.Value;
                var method = context.HttpContext.Request.Method;
                _logger.LogInformation(
                    "{method} {controllerName} {actionName} {identityName} {path} {queryString}", 
                    method,
                    controllerName,
                    actionName,
                    identityName,
                    path,
                    queryString);
            }
            await next();
        }
    }
}
