using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.WebSetup.Utils;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public bool IsEReportsInstallation { get; set; }

        public HomeController(IConfiguration configuration)
        {
            IsEReportsInstallation = configuration.GetValue<bool>("IsEReportsInstallation", false);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.IsEReportsInstallation = IsEReportsInstallation;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Info()
        {
            return View();
        }

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
