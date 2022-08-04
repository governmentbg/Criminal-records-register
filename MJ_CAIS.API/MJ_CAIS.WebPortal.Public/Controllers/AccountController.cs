using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            // Clear the existing external cookie
            return new SignOutResult(new[]
                    {
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        OpenIdConnectDefaults.AuthenticationScheme
                    },
                    new AuthenticationProperties() { RedirectUri = "/" }
                );
        }

        [AllowAnonymous]
        public IActionResult ErrorAuthentication()
        {
            return View();
        }
    }
}
