using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MJ_CAIS.WebPortal.External.Controllers
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
            ViewData["Controller"] = this;

            base.OnActionExecuting(filterContext);
        }

        public ClaimsIdentity CurrentUser => User?.Identity as ClaimsIdentity;

        public string? CurrentUserName => CurrentUser.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

        public string? CurrentRoleName => CurrentUser.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;

        public string? CurrentUserID => CurrentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? CurrentEgnIdentifier => CurrentUser.FindFirst("EgnIdentifier")?.Value;
    }
}
