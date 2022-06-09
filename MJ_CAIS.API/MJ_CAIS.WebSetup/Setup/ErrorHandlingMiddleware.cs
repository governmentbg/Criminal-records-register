using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;

namespace MJ_CAIS.WebSetup.Setup
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var userId = context.User.GetUserId();
            logger.LogError(ex, "{Message} {UserId}", ex.Message, userId);


            var (error, customMessage, statusCode) = GetError(context, ex);

            var result = JsonSerializer.Serialize(new { error, customMessage });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(result);

            //context.Response.Redirect("/Error");
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

        private bool IsAjax(HttpContext context)
        {
            return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
