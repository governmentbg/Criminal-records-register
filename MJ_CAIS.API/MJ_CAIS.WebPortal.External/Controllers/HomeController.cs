using Microsoft.AspNetCore.Mvc;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[RedirectAuthenticatedRequests("Index", "Application")] // TODO: uncomment
        public IActionResult Index()
        {
            return View();
        }
    }
}
