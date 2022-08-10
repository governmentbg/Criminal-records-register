using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.WebSetup.Utils;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[RedirectAuthenticatedRequests("Index", "Application")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Info()
        {
            return View();
        }

        [Authorize]
        public IActionResult Login()
        {
            
            return View("Index");
        }

        [AllowAnonymous]
        public IActionResult GeneralTerms()
        {
            return View();
        }
    }
}
