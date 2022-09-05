using AutoMapper;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Common.Validators;
using MJ_CAIS.DataAccess.ExtUsers;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Account;
using MJ_CAIS.WebPortal.External.Models.UserExternal;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [Authorize("Admin")]
    public class UserExternalController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserExternalService _externalService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;
        private readonly UserManager<LocalGUsersExt> _userManager;

        public UserExternalController(IMapper mapper,
                                     IUserExternalService externalService,
                                     INomenclatureDetailService nomenclatureDetailService,
                                     UserManager<LocalGUsersExt> userManager)
        {
            _mapper = mapper;
            _externalService = externalService;
            _nomenclatureDetailService = nomenclatureDetailService;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new UserExternalViewModel();
            return View(viewModel);
        }

        [HttpGet]
        [GridDataSourceAction]
        public async Task<ActionResult> GetUsers()
        {
            var result = await _externalService.SelectExternalUsersByUserIdAsync(CurrentUserID);
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> NewWithPassword()
        {
            var viewModel = new UserExternalPasswordNewModel();
            viewModel.IsAdded = true; 
             
            await FillDataForPasswordEditModel(viewModel);
            return View("NewWithPassword", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> NewWithPassword(UserExternalPasswordNewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await FillDataForPasswordEditModel(viewModel);
                return View(viewModel);
            }

            var itemToUpdate = new LocalGUsersExt()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = CurrentUserID,
                CreatedOn = DateTime.Now,
                Email = viewModel.Email,
                IsAdmin = viewModel.IsAdmin,
                Name = viewModel.Name,
                UserName = viewModel.UserName,
                Version = 1,
                Position = viewModel.Position,
                PhoneNumber = viewModel.Phone,
                Active = viewModel.Active
            };
            itemToUpdate.AdministrationId = await _externalService.GetUserAdministrationIdAsync(this.CurrentUserID);
            var result = await _userManager.CreateAsync(itemToUpdate, viewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                viewModel.CreateErrors = result.Errors;
                return View("NewWithPassword", viewModel);
            }
        }
        [HttpGet]
        public async Task<ActionResult> New(bool? password = false)
        {
            var viewModel = new UserExternalEditModel();

            await FillDataForEditModel(viewModel);
            return View(nameof(New), viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await this._externalService.GetUserExternalDTOAsync(id);
            if (string.IsNullOrEmpty(user.Egn))
            {
                var userWithPass = await _userManager.FindByIdAsync(id);
                var viewModel = new UserExternalPasswordEditModel()
                {
                    Id = id,
                    Email = userWithPass.Email,
                    Phone = userWithPass.PhoneNumber,
                    IsAdmin = userWithPass.IsAdmin.HasValue && userWithPass.IsAdmin.Value,
                    Active = userWithPass.Active.HasValue && userWithPass.Active.Value,
                    Name = userWithPass.Name,
                    Position = userWithPass.Position,
                    Version = userWithPass.Version.ToString()
                };
                await FillDataForPasswordEditModel(viewModel);
                return View("EditWithPassword", viewModel);

            }
            else
            {
                var viewModel = _mapper.Map<UserExternalDTO, UserExternalEditModel>(user);
                await FillDataForEditModel(viewModel);
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Password(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(new ResetPasswordViewModel() { Id = id, UserName = user.UserName });
        }

        [HttpPost]
        public async Task<ActionResult> Password(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            model.PasswordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
            model.UserName = user.UserName;
            model.Password = string.Empty;
            model.ConfirmPassword = string.Empty;
            return View(model);
        }

        //[HttpGet]
        //public async Task<ActionResult> Preview(string id)
        //{
        //    var user = await this._externalService.GetUserExternalDTOAsync(id);
        //    var viewModel = _mapper.Map<UserExternalDTO, UserExternalEditModel>(user);

        //    await FillDataForEditModel(viewModel);
        //    return View(viewModel);
        //}
        [HttpPost]
        public async Task<ActionResult> New(UserExternalEditModel viewModel)
        {
            var isValid = EgnValidator.IsValid(viewModel.Egn);
            if (!isValid)
            {
                ModelState.AddModelError(nameof(viewModel.Egn), FluentValidationResources.InvalidEgn);
            }

            if (!ModelState.IsValid)
            {
                await FillDataForEditModel(viewModel);
                return View(viewModel);
            }

            var itemToUpdate = _mapper.Map<UserExternalDTO>(viewModel);
            itemToUpdate.AdministrationId = await _externalService.GetUserAdministrationIdAsync(this.CurrentUserID);
            await _externalService.SaveUserExternalAsync(itemToUpdate, true);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserExternalEditModel viewModel)
        {
            var isValid = EgnValidator.IsValid(viewModel.Egn);
            if (!isValid)
            {
                ModelState.AddModelError(nameof(viewModel.Egn), FluentValidationResources.InvalidEgn);
            }

            if (!ModelState.IsValid)
            {
                await FillDataForEditModel(viewModel);
                return View(viewModel);
            }

            var itemToUpdate = _mapper.Map<UserExternalDTO>(viewModel);
            itemToUpdate.AdministrationId = await _externalService.GetUserAdministrationIdAsync(this.CurrentUserID);
            await _externalService.SaveUserExternalAsync(itemToUpdate, false);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<ActionResult> EditWithPassword(UserExternalPasswordEditModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await FillDataForPasswordEditModel(viewModel);
                return View(viewModel);
            }
            var itemToUpdate = await _userManager.FindByIdAsync(viewModel.Id);

            itemToUpdate.Email = viewModel.Email;
            itemToUpdate.IsAdmin = viewModel.IsAdmin;
            itemToUpdate.Name = viewModel.Name;
            itemToUpdate.Active = viewModel.Active;
            itemToUpdate.Position = viewModel.Position;
            itemToUpdate.PhoneNumber = viewModel.Phone;
            itemToUpdate.ConcurrencyStamp = viewModel.ConcurrencyStamp;

            await _userManager.UpdateAsync(itemToUpdate);
            return RedirectToAction(nameof(Index));
        }

        private async Task FillDataForEditModel(UserExternalEditModel viewModel)
        {
            var administrationName = await _externalService.GetUserAdministrationNameAsync(this.CurrentUserID);
            viewModel.AdministrationName = administrationName;
        }
        private async Task FillDataForPasswordEditModel(UserExternalPasswordEditModel viewModel)
        {
            var administrationName = await _externalService.GetUserAdministrationNameAsync(this.CurrentUserID);
            viewModel.AdministrationName = administrationName;
        }
    }
}
