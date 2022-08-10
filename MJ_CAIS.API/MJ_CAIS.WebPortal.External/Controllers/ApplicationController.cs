using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Common.Validators;
using MJ_CAIS.DTO.Application.External;
using MJ_CAIS.ExternalWebServices.DbServices;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Application;
using MJ_CAIS.WebPortal.External.Models.Reports;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [RedirectNotActive("Inactive", "Account")]
    [Authorize]
    public class ApplicationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IApplicationWebService _applicationWebService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;
        private readonly ICertificateService _certificateService;
        private readonly IRegixService _regixService;
        private readonly ICriminalRecordsReportService _criminalRecordsReportService;

        public ApplicationController(IMapper mapper,
                                     IApplicationWebService applicationWebService,
                                     INomenclatureDetailService nomenclatureDetailService,
                                     ICertificateService certificateService,
                                     IRegixService regixService,
                                     ICriminalRecordsReportService criminalRecordsReportService)
        {
            _mapper = mapper;
            _applicationWebService = applicationWebService;
            _nomenclatureDetailService = nomenclatureDetailService;
            _certificateService = certificateService;
            _regixService = regixService;
            _criminalRecordsReportService = criminalRecordsReportService;
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
            viewModel.Email = CurrentUserEmail;

            var itemToUpdate = _mapper.Map<ExternalApplicationDTO>(viewModel);
            var id = await _applicationWebService.InsertExternalAsync(itemToUpdate);
            _regixService.CreateRegixRequests(viewModel.Egn, wApplicationId: id);

            TempData["createSuccessfull"] = true;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Preview(string id)
        {
            var app = await _applicationWebService.GetExternalForPreviewAsync(id);
            var viewModel = _mapper.Map<ApplicationPreviewModel>(app);
            viewModel.HasGeneratedCertificate = app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint ||
                 app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.CertificateForDelivery || app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.Delivered;

            return View(viewModel);
        }

        [HttpGet]
        [GridDataSourceAction]
        public async Task<ActionResult> GetUserApplications()
        {
            var result = _applicationWebService.SelectExternalApplications(CurrentUserID);
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> DownloadCertificate(string id)
        {
            var result = await this._certificateService.GetCertificateContentByWebAppIdAsync(id);

            if (result == null)
                throw new BusinessLogicException(BusinessLogicExceptionResources.certificateDoesNotExist);

            var content = result;
            var fileName = "certificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        private async Task FillDataForEditModel(ApplicationEditModel viewModel)
        {
            var purposes = await _nomenclatureDetailService.GetAllAPurposes().ToListAsync();
            viewModel.PurposeInfo = purposes.Where(p => p.RequestInfo.HasValue && p.RequestInfo.Value).ToDictionary(o => o.Code, o => o.Description);
            viewModel.PurposeTypes = purposes.Select(p => new SelectListItem() { Value = p.Code, Text = p.Name }).ToList();
            viewModel.PurposeTypes.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = true });
            viewModel.RequiredPurposes = string.Join(",", viewModel.PurposeInfo.Keys);
            var paymentMethods = _nomenclatureDetailService.GetWebAPaymentMethods();
        }
    }
}
