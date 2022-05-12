using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MJ_CAIS.Common.Exceptions;
using System.Diagnostics;

namespace MJ_CAIS.WebPortal.Public.Pages
{
    // TODO: remove asp page, and make controller
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        private ILogger<ErrorModel> _logger;

        public string FormatedMessage { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionResult = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = exceptionResult.Error;
            _logger.LogError(error, error.Message);

            this.FormatedMessage = ExceptionUtils.GetFormatedLastError(error);
        }

        public void OnPost()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;


            var exceptionResult = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = exceptionResult.Error;
            _logger.LogError(error, error.Message);

            this.FormatedMessage = ExceptionUtils.GetFormatedLastError(error);
        }
    }
}