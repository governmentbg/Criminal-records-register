using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.WebPortal.Public.Models.Application;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new ApplicationViewModel();
            return View(viewModel);
        }

        [HttpGet]
        [GridDataSourceAction]
        public ActionResult GetUserApplications()
        {
            var result = new List<PublicApplicationGridDTO>
            {
                new PublicApplicationGridDTO { Id = "aaa", RegistrationNumber = "123" },
                new PublicApplicationGridDTO { Id = "bbb", RegistrationNumber = "124" },
                new PublicApplicationGridDTO { Id = "ccc", RegistrationNumber = "125" },
            }.AsQueryable();
            return View(result);
        }
    }
}
