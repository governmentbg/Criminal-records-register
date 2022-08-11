using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.WebSetup.Utils;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
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
