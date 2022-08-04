using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.ExternalWebServices.DbServices;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Application;
using MJ_CAIS.WebSetup.Utils;
using Newtonsoft.Json;
using TL.EGovPayments;
using TL.EGovPayments.ControllerModels;
using TL.EGovPayments.Interfaces;
using TL.EGovPayments.JsonEnums;
using TL.EGovPayments.JsonModels;
using X.PagedList;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IApplicationWebService _applicationWebService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;
        private readonly RequestUtils _requestUtils;
        private readonly ICertificateService _certificateService;
        private readonly IRegixService _regixService;
        private readonly IEGovPaymentService _egovPaymentService;
        private readonly IEGovIntegrationService _egovIntegrationService;
        private readonly CaisDbContext _dbContext;

        public ApplicationController(IMapper mapper,
                                     IApplicationWebService applicationWebService,
                                     INomenclatureDetailService nomenclatureDetailService,
                                     RequestUtils requestUtils,
                                     ICertificateService certificateService,
                                     IRegixService regixService,
                                     IServiceProvider serviceProvider,
                                     IEGovPaymentService egovPaymentService,
                                     IEGovIntegrationService egovIntegrationService,
                                     CaisDbContext dbContext
                                     )
        {
            _mapper = mapper;
            _applicationWebService = applicationWebService;
            _nomenclatureDetailService = nomenclatureDetailService;
            _requestUtils = requestUtils;
            _certificateService = certificateService;
            _regixService = regixService;
            _egovPaymentService = egovPaymentService;
            _egovIntegrationService = egovIntegrationService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var result = _applicationWebService.SelectPublicApplications(CurrentUserID);
            return View(result.ToPagedList(page ?? 1, 10));
        }

        [HttpGet]
        public async Task<ActionResult> New()
        {
            var viewModel = new ApplicationEditModel();
            viewModel.Egn = CurrentEgnIdentifier;
            viewModel.Email = CurrentMail;

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
            var id =  await _applicationWebService.InsertPublicAsync(itemToUpdate);

            //var result = _regixService.SyncCallPersonDataSearch(viewModel.Egn, wApplicationId: id, isAsync: true);
            _regixService.CreateRegixRequests(viewModel.Egn, wApplicationId: id);

            var paymentMethod = _nomenclatureDetailService.GetWebAPaymentMethods().Where(pm => pm.Id == viewModel.PaymentMethodId).FirstOrDefault();
            if (paymentMethod != null && paymentMethod.Code == "PayEgovBg")
            {
                var price = await _applicationWebService.GetPriceByApplicationType("4");
                var application = await _applicationWebService.SelectAsync(id);
                var paymentRequestModel = new EGovPaymentRequestModel()
                {
                    ApplicantIdentifier = CurrentEgnIdentifier,
                    ApplicantName = CurrentUserName,
                    ApplicantType = ApplicantTypes.EGN,
                    MobilePayment = false,
                    PaymentAmount = (float)price,
                    PaymentReason = "Такса свидетелство съдимост",
                    PaymentRefDate = DateTime.Now,
                    PaymentRefNumber = application.RegistrationNumber,
                    PaymentType = VPOSPaymentTypes.EPAY
                };
                TempData["paymentRequestModel"] = JsonConvert.SerializeObject(paymentRequestModel);
                return RedirectToActionPreserveMethod("CreateVPOSPayment2", "EGovPayments");
            }
            //TODO: 
            //else if (paymentMethod !=  null && paymentMethod.Code == "FreeFromTax")
            //{
            //}
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<ActionResult> Preview(string id)
        {
            var app = await _applicationWebService.GetPublicForPreviewAsync(id);
            var viewModel = _mapper.Map<ApplicationPreviewModel>(app);
            viewModel.HasGeneratedCertificate = app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint ||
                app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.CertificateForDelivery || app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.Delivered;

            return View(viewModel);
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

        [HttpGet]
        public async Task<ActionResult> PaymentOk([FromQuery] string requestId)
        {
            string? paymentStatus = await GetPaymentStatus(requestId);
            var model = new PaymentResultViewModel() { Canceled = false, PaymentStatus = paymentStatus };
            return View("Payment", model);
        }

        private async Task<string> GetPaymentStatus(string requestId)
        {
            var userId = CurrentUserID;
            var paymentStatus = await (from wa in _dbContext.WApplications
                                       join ap in _dbContext.APayments on wa.Id equals ap.WApplicationId
                                       join p in _dbContext.EPayments on ap.EPaymentId equals p.Id
                                       where p.InvoiceNumber == requestId &&
                                             wa.UserCitizenId == userId
                                       select p.PaymentStatus).FirstOrDefaultAsync();
            return paymentStatus;
        }

        [HttpGet]
        public async Task<ActionResult> PaymentCancel([FromQuery] string requestId)
        {
            string? paymentStatus = await GetPaymentStatus(requestId);
            var model = new PaymentResultViewModel() { Canceled = true, PaymentStatus = paymentStatus };
            return View("Payment", model);
        }

        //[HttpGet]
        //[GridDataSourceAction]
        //public ActionResult GetUserApplications()
        //{
        //    var result = _applicationWebService.SelectPublicApplications(CurrentUserID);
        //    return View(result);
        //}

        private async Task FillDataForEditModel(ApplicationEditModel viewModel)
        {
            var purposes = _nomenclatureDetailService.GetAllAPurposes();
            viewModel.PurposeTypes = await purposes.ProjectTo<SelectListItem>(
                _mapper.ConfigurationProvider).ToListAsync();
            viewModel.PurposeTypes.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = true});

            var paymentMethods = _nomenclatureDetailService.GetWebAPaymentMethods();
            viewModel.PaymentMethodTypes = await paymentMethods.ProjectTo<SelectListItem>(
                _mapper.ConfigurationProvider).ToListAsync();
            viewModel.PaymentMethodTypes.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = true });
        }
    }
}
