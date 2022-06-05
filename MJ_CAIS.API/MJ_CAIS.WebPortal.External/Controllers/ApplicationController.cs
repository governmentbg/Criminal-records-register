using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Application.External;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Application;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IApplicationWebService _applicationWebService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;

        public ApplicationController(IMapper mapper,
                                     IApplicationWebService applicationWebService,
                                     INomenclatureDetailService nomenclatureDetailService)
        {
            _mapper = mapper;
            _applicationWebService = applicationWebService;
            _nomenclatureDetailService = nomenclatureDetailService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new ApplicationViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> New()
        {
            var viewModel = new ApplicationEditModel();

            await FillDataForEditModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> New(ApplicationEditModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await FillDataForEditModel(viewModel);
                return View(viewModel);
            }

            var itemToUpdate = _mapper.Map<ExternalApplicationDTO>(viewModel);
            await _applicationWebService.InsertExternalAsync(itemToUpdate);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [GridDataSourceAction]
        public async Task<ActionResult> GetUserApplications()
        {
            var result = _applicationWebService.SelectExternalApplications(CurrentUserID);
            return View(result);
        }

        private async Task FillDataForEditModel(ApplicationEditModel viewModel)
        {
            var purposes = _nomenclatureDetailService.GetAllAPurposes();
            viewModel.PurposeTypes = await purposes.ProjectTo<SelectListItem>(
                _mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
