using IdentityModel;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                var identityId = context?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value;
                var path = context?.HttpContext?.Request?.Path;
                var queryString = context?.HttpContext?.Request?.QueryString.Value;
                var method = context?.HttpContext?.Request?.Method;
                var remoteIP = context?.HttpContext?.Connection?.RemoteIpAddress;
                _logger.LogInformation(
                    "{method} {controllerName} {actionName} {identityName}({identityId}) {path} {queryString} {remoteIP}", 
                    method,
                    controllerName,
                    actionName,
                    identityName,
                    identityId,
                    path,
                    queryString,
                    remoteIP);
            }
            await next();
        }
    }
}
