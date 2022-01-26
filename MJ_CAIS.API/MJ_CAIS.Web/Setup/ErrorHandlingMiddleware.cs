using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Web.Identity;
using System.Net;
using System.Text.Json;

namespace MJ_CAIS.Web.Setup
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (error, customMessage, statusCode) = GetError(context, ex);

            var result = JsonSerializer.Serialize(new
            {
                error = error,
                customMessage = customMessage
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var UserId = context.User.GetUserId();
            logger.LogError(ex, "{Message} {UserId}", ex.Message, UserId);

            return context.Response.WriteAsync(result);
        }

        private (string, string, HttpStatusCode) GetError(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var error = ex.Message;
            var customMessage = "";

            switch (ex)
            {
                case DbUpdateException updateException:
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        var currentEx = updateException.InnerException ?? ex;
                        var httpMethod = context.Request.Method;
                        if (currentEx.Message.ToLower().Contains("violates foreign key constraint") && httpMethod == HttpMethods.Delete)
                        {
                            customMessage = "Невъзможно изтриване. Този запис се реферира от други обекти!";
                        }

                        break;
                    }
                case BusinessLogicException businessLogicException:
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        customMessage = businessLogicException.Message;
                        break;
                    }
            }

            return (error, customMessage, statusCode);
        }
    }
}
