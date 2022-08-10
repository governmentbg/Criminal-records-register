using AutoMapper;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Common.Validators;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.UserExternal;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    //[Authorize]
    public class UserExternalController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserExternalService _externalService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;

        public UserExternalController(IMapper mapper,
                                     IUserExternalService externalService,
                                     INomenclatureDetailService nomenclatureDetailService)
        {
            _mapper = mapper;
            _externalService = externalService;
            _nomenclatureDetailService = nomenclatureDetailService;
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
        public async Task<ActionResult> New()
        {
            var viewModel = new UserExternalEditModel();
            viewModel.IsAdded = true;

            await FillDataForEditModel(viewModel);
            return View(nameof(Edit), viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await this._externalService.GetUserExternalDTOAsync(id);
            var viewModel = _mapper.Map<UserExternalDTO, UserExternalEditModel>(user);
            viewModel.IsAdded = false;

            await FillDataForEditModel(viewModel);
            return View(viewModel);
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
            await _externalService.SaveUserExternalAsync(itemToUpdate, viewModel.IsAdded);

            return RedirectToAction(nameof(Index));
        }

        private async Task FillDataForEditModel(UserExternalEditModel viewModel)
        {
            var administrationName = await _externalService.GetUserAdministrationNameAsync(this.CurrentUserID);
            viewModel.AdministrationName = administrationName;
        }
    }
}
