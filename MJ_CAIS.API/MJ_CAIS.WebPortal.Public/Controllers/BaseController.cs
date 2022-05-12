using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MJ_CAIS.WebSetup.Utils;
using System.Security.Claims;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class BaseController : Controller
    {
        public string Culture { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (TempData != null)
                TempData["CallerUrl"] = Request.Headers["Referer"].ToString();

            var cultureName = "bg-BG";

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            this.Culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

            ViewData["Culture"] = this.Culture;
            ViewData["IsAuthenticated"] = CurrentUser != null && CurrentUser.IsAuthenticated;

            base.OnActionExecuting(filterContext);
        }

        public ClaimsIdentity CurrentUser => User?.Identity as ClaimsIdentity; // TODO: CaisIdentity
    }
}
