using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Application;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;

        public ApplicationController(IMapper mapper,
                                     IApplicationService applicationService,
                                     INomenclatureDetailService nomenclatureDetailService)
        {
            _mapper = mapper;
            _applicationService = applicationService;
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
            viewModel.Egn = CurrentEgnIdentifier;

            await FillDataForEditModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> New(ApplicationEditModel viewModel)
        {
            // Always applied
            viewModel.Egn = CurrentEgnIdentifier;

            // TODO:
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [GridDataSourceAction]
        public async Task<ActionResult> GetUserApplications()
        {
            var result = _applicationService.SelectPublicApplications(CurrentUserID);
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
