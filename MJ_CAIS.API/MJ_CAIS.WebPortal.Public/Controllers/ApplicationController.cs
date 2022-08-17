using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.DTO.NomenclatureDetail;
using MJ_CAIS.ExternalWebServices.DbServices;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Application;
using MJ_CAIS.WebPortal.Public.Services;
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
        public const string WEB_APPLICATION_TYPE = "4";
        private readonly IMapper _mapper;
        private readonly IApplicationWebService _applicationWebService;
        private readonly INomenclatureDetailService _nomenclatureDetailService;
        private readonly RequestUtils _requestUtils;
        private readonly ICertificateService _certificateService;
        private readonly IRegixService _regixService;
        private readonly IEGovPaymentService _egovPaymentService;
        private readonly ICAISEGovIntegrationService _egovIntegrationService;
        private readonly CaisDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ApplicationController(IMapper mapper,
                                     IApplicationWebService applicationWebService,
                                     INomenclatureDetailService nomenclatureDetailService,
                                     RequestUtils requestUtils,
                                     ICertificateService certificateService,
                                     IRegixService regixService,
                                     IServiceProvider serviceProvider,
                                     IEGovPaymentService egovPaymentService,
                                     ICAISEGovIntegrationService egovIntegrationService,
                                     CaisDbContext dbContext,
                                     IConfiguration configuration
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
            _configuration = configuration;
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
            var id = await _applicationWebService.InsertPublicAsync(itemToUpdate);

            //var result = _regixService.SyncCallPersonDataSearch(viewModel.Egn, wApplicationId: id, isAsync: true);
            _regixService.CreateRegixRequests(viewModel.Egn, wApplicationId: id);

            var paymentMethod = _nomenclatureDetailService.GetWebAPaymentMethods().Where(pm => pm.Id == viewModel.PaymentMethodId).FirstOrDefault();
            if (paymentMethod != null && (paymentMethod.Code == "PayEgovBg" || paymentMethod.Code == "PayEgovBgEPay"))
            {
                var price = await _applicationWebService.GetPriceByApplicationType(WEB_APPLICATION_TYPE);
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
                    PaymentType = (paymentMethod.Code == "PayEgovBgEPay") ? VPOSPaymentTypes.EPAY : VPOSPaymentTypes.BANK
                };
                TempData["paymentRequestModel"] = JsonConvert.SerializeObject(paymentRequestModel);
                return RedirectToActionPreserveMethod("CreateVPOSPayment2", "EGovPayments");
            }
            else if (paymentMethod != null && paymentMethod.Code == "PayEgovBgCode")
            {
                var price = await _applicationWebService.GetPriceByApplicationType(WEB_APPLICATION_TYPE);
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
                    PaymentType = (paymentMethod.Code == "PayEgovBgEPay") ? VPOSPaymentTypes.EPAY : VPOSPaymentTypes.BANK
                };

                PaymentReceipt receipt = await CreatePaymentRequest(paymentRequestModel);
                string accessCode = await _egovPaymentService.GetPaymentRequestAccessCode(receipt.ID);
                _egovIntegrationService.SavePaymentId(paymentRequestModel.PaymentRefNumber, receipt.ID, accessCode);

                return RedirectToAction(nameof(Preview), new { id = id, paymentStatus = "PayEgovBgCode" });
            }
            else
            {
                return RedirectToAction(nameof(Preview), new { id = id, paymentStatus = "TaxFreeOrBank" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Preview(string id, [FromQuery] string paymentStatus = "")
        {
            var app = await _applicationWebService.GetPublicForPreviewAsync(id);
            var viewModel = _mapper.Map<ApplicationPreviewModel>(app);
            viewModel.ReturnFromPaymentResult = paymentStatus;
            viewModel.PayEgovBGCodeLink = $"{_configuration.GetValue<string?>("EGovPayments:VPOS:BasePortalUrl")}/Home/AccessByCode?code={viewModel.PayEgovBGCode}";
            viewModel.ServiceProviderBank = _configuration.GetValue<string?>("EGovPayments:ServiceProviderBank");
            viewModel.ServiceProviderBIC = _configuration.GetValue<string?>("EGovPayments:ServiceProviderBIC");
            viewModel.ServiceProviderIBAN = _configuration.GetValue<string?>("EGovPayments:ServiceProviderIBAN");
            viewModel.ServiceProviderName = _configuration.GetValue<string?>("EGovPayments:ServiceProviderName");
            viewModel.HasGeneratedCertificate = app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint ||
                app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.CertificateForDelivery || app.CertificateStatusCode == ApplicationConstants.ApplicationStatuses.Delivered;

            return View(viewModel);
        }

        private async Task<PaymentReceipt> CreatePaymentRequest(EGovPaymentRequestModel paymentRequest)
        {
            PaymentRequest request = new PaymentRequest
            {
                PaymentReason = paymentRequest.PaymentReason,
                PaymentAmount = paymentRequest.PaymentAmount,
                PaymentReferenceDate = paymentRequest.PaymentRefDate,
                PaymentReferenceNumber = paymentRequest.PaymentRefNumber,
                ApplicantUinTypeId = paymentRequest.ApplicantType.Value,
                ApplicantUin = paymentRequest.ApplicantIdentifier,
                ApplicantName = paymentRequest.ApplicantName,
            };

            if (!paymentRequest.PaymentRequestExpiration.HasValue)
            {
                request.ExpirationDate = DateTime.Now.Date.AddDays(EGovPaymentSettings.Default.PaymentRequestExpirationDays);
            }

            request.AisPaymentId = _egovIntegrationService.GeneratePaymentNumber(request);
            return await _egovPaymentService.CreatePaymentRequest(request);
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
            string? applicationId = await GetWApplicationId(requestId);
            return RedirectToAction("Preview", "Application", new { id = applicationId, paymentStatus = "OK" });
        }

        private async Task<string> GetWApplicationId(string requestId)
        {
            var userId = CurrentUserID;
            var wApplicationId = await (from wa in _dbContext.WApplications
                                       join ap in _dbContext.APayments on wa.Id equals ap.WApplicationId
                                       join p in _dbContext.EPayments on ap.EPaymentId equals p.Id
                                       where p.InvoiceNumber == requestId &&
                                             wa.UserCitizenId == userId
                                       select wa.Id).FirstOrDefaultAsync();
            return wApplicationId;
        }

        [HttpGet]
        public async Task<ActionResult> PaymentCancel([FromQuery] string requestId)
        {
            string? applicationId = await GetWApplicationId(requestId);
            return RedirectToAction("Preview", "Application", new { id = applicationId, paymentStatus = "Cancel" });
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
            viewModel.Egn = CurrentEgnIdentifier;
            var purposes = await _nomenclatureDetailService.GetAllAPurposes().ToListAsync();
            viewModel.Price = await _applicationWebService.GetPriceByApplicationType(WEB_APPLICATION_TYPE);
            viewModel.PurposeInfo = purposes.Where(p => p.RequestInfo.HasValue && p.RequestInfo.Value).ToDictionary( o => o.Code, o => o.Description);
            viewModel.PurposeTypes = purposes.Select( p =>  new SelectListItem() { Value = p.Code, Text = p.Name}).ToList();
            viewModel.PurposeTypes.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = true});
            viewModel.RequiredPurposes = string.Join(",", viewModel.PurposeInfo.Keys);
            viewModel.PaymentMethodTypes = await _nomenclatureDetailService.GetWebAPaymentMethods().ToListAsync();
        }
    }
}
