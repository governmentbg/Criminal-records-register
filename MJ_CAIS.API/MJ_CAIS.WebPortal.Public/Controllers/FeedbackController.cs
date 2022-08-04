using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Feedback;
using System.Text.Encodings.Web;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class FeedbackController : BaseController
    {
        private readonly IEEMailEventService _emailEventService;
        public FeedbackController(IEEMailEventService emailEventService)
        {
            _emailEventService = emailEventService;
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
        public ActionResult Send(FeedbackViewModel viewModel,[FromServices]  HtmlEncoder htmlEncoder)
        {
            var name = htmlEncoder.Encode(viewModel.Name ?? "");
            var feedbackForId = htmlEncoder.Encode(viewModel.FeedbackForId ?? "");
            var description = htmlEncoder.Encode(viewModel.Description ?? "");

            _emailEventService.AddEmailEvent(
                //TODO: Get mail group from settings
                "dmitev@technologica.com",
                !string.IsNullOrEmpty(name) ? 
                $"{feedbackForId} - {name}" :
                feedbackForId,
                description
            );
            return View("Success");
        }
    }
}
