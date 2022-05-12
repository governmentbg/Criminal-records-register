using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.WebPortal.Public.Models;
using MJ_CAIS.WebSetup.Utils;
using System.Diagnostics;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [RedirectAuthenticatedRequests("Index", "Application")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
