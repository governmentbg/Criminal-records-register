using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MJ_CAIS.DTO.User;
using MJ_CAIS.DTO.UserCitizen;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebSetup.Utils;
using System.Security.Claims;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserCitizenService _userCitizenService;

        public AccountController(IUserCitizenService userCitizenService)
        {
            _userCitizenService = userCitizenService;
        }

        [HttpGet]
        //[AllowAnonymous]
        [RedirectAuthenticatedRequests("Index", "Application")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [RedirectAuthenticatedRequests("Index", "Application")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Login()
        {
            var returnUrl = "";

            var userDTO = new UserCitizenDTO
            {
                Egn = "9701010101",
                Name = "Иван Иванов Петров",
                Email = "ivan.ivanov@test.bg",
            };

            if (ModelState.IsValid)
            {
                var user = await _userCitizenService.AuthenticatePublicUserAsync(userDTO);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("EgnIdentifier", user.Egn),
                };

                await SingInWithHttpContext(claims);

                return LocalRedirect(GetLocalUrl(returnUrl));
            }

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> LogOff()
        {
            // Clear the existing external cookie
            return new SignOutResult(new[]
                    {
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        OpenIdConnectDefaults.AuthenticationScheme
                    },
                    new AuthenticationProperties() { RedirectUri = "/"}
                );
        }

        private string GetLocalUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            else
            {
                return Url.Action("Index", "Application");
            }
        }

        private async Task SingInWithHttpContext(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
