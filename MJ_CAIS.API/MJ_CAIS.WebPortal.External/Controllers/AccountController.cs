using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Account;
using System.Linq;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserExternalService _externalUsers;
        private readonly IExtAdministrationService _extAdministrationService;

        public AccountController(
            IMapper mapper,
            IExtAdministrationService extAdministrationService,
            IUserExternalService externalUsers)
        {
            _mapper = mapper;
            _extAdministrationService = extAdministrationService;
            _externalUsers = externalUsers;
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

        [HttpGet]
        public async Task<ActionResult> Inactive()
        {
            var userData = await _externalUsers.SelectAsync(CurrentUserID);
            var administrations = await _extAdministrationService.SelectAllAsync();
            var model = _mapper.Map<InactiveViewModel>(userData);
            model.Administrations = administrations.OrderBy(a => a.Name).Select(a => new SelectListItem(a.Name, a.Id, (model.AdministrationId == a.Id))).ToList();
            model.Administrations.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = (model.AdministrationId == null) });
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Inactive(InactiveViewModel viewModel)
        {
            var userData = await _externalUsers.SelectAsync(CurrentUserID);
            var itemToUpdate = _mapper.Map<UserExternalDTO>(viewModel);
            itemToUpdate.Id = CurrentUserID;
            itemToUpdate.Egn = userData.Egn;
            try
            {
                await _externalUsers.UpdateAsync(CurrentUserID, itemToUpdate);
                return RedirectToAction("InactiveNotification", new { success = true });
            }
            catch(DbUpdateConcurrencyException ce)
            {
                return RedirectToAction("InactiveNotification", new { success = false });
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> InactiveNotification(bool success)
        {
            return View(new InactiveNotificationModel() { Result = success });
        }

        [AllowAnonymous]
        public IActionResult ErrorAuthentication()
        {
            return View();
        }
    }
}
