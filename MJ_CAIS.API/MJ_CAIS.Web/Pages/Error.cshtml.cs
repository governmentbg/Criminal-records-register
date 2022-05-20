using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MJ_CAIS.Common.Exceptions;
using System.Diagnostics;

namespace WebApplicationNet6.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public string FormatedMessage { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            this.ProcessError();
        }

        public void OnPost()
        {
            this.ProcessError();
        }

        public void OnPut()
        {
            this.ProcessError();
        }

        private void ProcessError()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionResult = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = exceptionResult.Error;
            _logger.LogError(error, error.Message);

            this.FormatedMessage = ExceptionUtils.GetFormatedLastError(error);
        }
    }
}