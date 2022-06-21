using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Application;
using MJ_CAIS.WebSetup.Utils;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IApplicationWebService _applicationWebService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;
        private readonly RequestUtils _requestUtils;

        public ApplicationController(IMapper mapper,
                                     IApplicationWebService applicationWebService,
                                     INomenclatureDetailService nomenclatureDetailService,
                                     RequestUtils requestUtils)
        {
            _mapper = mapper;
            _applicationWebService = applicationWebService;
            _nomenclatureDetailService = nomenclatureDetailService;
            _requestUtils = requestUtils;
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
            if (!ModelState.IsValid)
            {
                await FillDataForEditModel(viewModel);
                return View(viewModel);
            }

            viewModel.Egn = CurrentEgnIdentifier;
            viewModel.ClientIp = _requestUtils.GetClientIpAddress(HttpContext);

            var itemToUpdate = _mapper.Map<PublicApplicationDTO>(viewModel);
            await _applicationWebService.InsertPublicAsync(itemToUpdate);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Preview(string id)
        {
            var app = await _applicationWebService.GetForPreviewAsync(id);
            var viewModel = _mapper.Map<ApplicationPreviewModel>(app);

            return View(viewModel);
        }

        [HttpGet]
        [GridDataSourceAction]
        public ActionResult GetUserApplications()
        {
            var result = _applicationWebService.SelectPublicApplications(CurrentUserID);
            return View(result);
        }

        private async Task FillDataForEditModel(ApplicationEditModel viewModel)
        {
            var purposes = _nomenclatureDetailService.GetAllAPurposes();
            viewModel.PurposeTypes = await purposes.ProjectTo<SelectListItem>(
                _mapper.ConfigurationProvider).ToListAsync();

            var paymentMethods = _nomenclatureDetailService.GetWebPaymentMethods();
            viewModel.PaymentMethodTypes = await paymentMethods.ProjectTo<SelectListItem>(
                _mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
