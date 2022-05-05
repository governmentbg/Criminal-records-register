using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.WebPortal.Public.Models.Application;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class ApplicationController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            throw new NotImplementedException();
            var viewModel = new ApplicationViewModel();
            return View(viewModel);
        }

        //[HttpGet]
        //[GridDataSourceAction]
        //public ActionResult GetApplications()
        //{
        //    var result = new List<ApplicationGridDTO>
        //    {
        //        new ApplicationGridDTO { Id = "aaa", RegistrationNumber = "123" },
        //        new ApplicationGridDTO { Id = "bbb", RegistrationNumber = "123" },
        //        new ApplicationGridDTO { Id = "ccc", RegistrationNumber = "123" },
        //    }.AsQueryable();
        //    return View(result);
        //}
    }
}
