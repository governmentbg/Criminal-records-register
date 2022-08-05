using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Feedback;
using System.Text.Encodings.Web;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class FeedbackController : BaseController
    {
        private readonly IEEMailEventService _emailEventService;
        private readonly IGSystemParameterService _systemParameterService;
        private readonly ILogger<FeedbackController> _logger;
        public FeedbackController(
            IEEMailEventService emailEventService,
            IGSystemParameterService systemParameterService,
            ILogger<FeedbackController> logger)
        {
            _emailEventService = emailEventService;
            _systemParameterService = systemParameterService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new FeedbackViewModel();
            viewModel.Email = CurrentMail;
            viewModel.Name = CurrentUserName;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Send(FeedbackViewModel viewModel,[FromServices]  HtmlEncoder htmlEncoder)
        {
            var name = htmlEncoder.Encode(viewModel.Name ?? "");
            var email = htmlEncoder.Encode(viewModel.Email ?? "");
            var feedbackForId = htmlEncoder.Encode(viewModel.FeedbackForId ?? "");
            var description = htmlEncoder.Encode(viewModel.Description ?? "");
            var to = await _systemParameterService.GetValueString("CRR_PUBLIC_FEEDBACK_EMAIL");
            if (string.IsNullOrEmpty(to))
            {
                _logger.LogError("CRR_PUBLIC_FEEDBACK_EMAIL system varaible not set!");
                return View("Failure");
            }
            var subject = await _systemParameterService.GetValueString("CRR_PUBLIC_FEEDBACK_SUBJECT");
            if (string.IsNullOrEmpty(subject))
            {
                _logger.LogError("CRR_PUBLIC_FEEDBACK_SUBJECT system varaible not set!");
                return View("Failure");
            }
            await _emailEventService.AddEmailEvent(
                to,
                subject,
                $"{FeedbackResources.lblName}: {name}({email})<br/>" +
                $"{FeedbackResources.lblFeedbackFor}: {feedbackForId}<br/>" +
                $"{FeedbackResources.lblDescription}: {description}"
            );
            return View("Success");
        }
    }
}
