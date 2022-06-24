using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using TL.EGovPayments;
using TL.EGovPayments.ControllerModels;
using TL.EGovPayments.Interfaces;
using TL.EGovPayments.JsonEnums;
using TL.EGovPayments.JsonModels;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    [Route(template: "[controller]/[action]")]
    public class EGovPaymentsController : TL.EGovPayments.EGovPaymentsController
    {
        private ITempDataDictionary? _tempData;

        public EGovPaymentsController(IConfiguration configuration,
                                      IEGovPaymentService paymentService,
                                      IEGovIntegrationService egovIntegrationService)
            : base(configuration, paymentService, egovIntegrationService)
        {
        }

        /// <summary>
        /// Gets or sets <see cref="ITempDataDictionary"/> used by <see cref="ViewResult"/>.
        /// </summary>
        public ITempDataDictionary TempData
        {
            get
            {
                if (_tempData == null)
                {
                    var factory = HttpContext?.RequestServices?.GetRequiredService<ITempDataDictionaryFactory>();
                    _tempData = factory?.GetTempData(HttpContext);
                }

                return _tempData!;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _tempData = value;
            }
        }

        //[HttpGet]
        ////[CustomAuthorize]
        //public override IActionResult CreateVPOSPayment(string paymentId, VPOSPaymentTypes paymentType, bool mobilePayment = false)
        //{
        //    return base.CreateVPOSPayment(paymentId, paymentType, mobilePayment);
        //}

        [HttpPost]
        //[CustomAuthorize]
        public async Task<IActionResult> CreateVPOSPayment2()
        {
            EGovPaymentRequestModel paymentRequest = JsonConvert.DeserializeObject<EGovPaymentRequestModel>(TempData["paymentRequestModel"].ToString());
            var res = await base.CreateVPOSPayment(paymentRequest);
            //return res;
            var objectResult = res as ObjectResult;
            var value = objectResult.Value;
            //TempData["paymentRequestModel2"] = JsonConvert.SerializeObject(value);
            return RedirectToAction("Index", "Application");
            //return RedirectToActionPreserveMethod("New2", "Application");
        }

        [HttpGet]
        //[CustomAuthorize]
        public override Task<IActionResult> CreateVPOSPaymentByRefNumber([FromQuery] string paymentRefNumber, [FromQuery] VPOSPaymentTypes paymentType, [FromQuery] bool mobilePayment = false)
        {
            return base.CreateVPOSPaymentByRefNumber(paymentRefNumber, paymentType, mobilePayment);
        }

        [HttpPost]
        //[CustomAuthorize]
        public override Task<IActionResult> RegisterOfflinePayment([FromBody] EGovPaymentRequestModel paymentRequest)
        {
            return base.RegisterOfflinePayment(paymentRequest);
        }

        [HttpGet]
        //[CustomAuthorize]
        public override Task<IActionResult> RegisterOfflinePayment([FromQuery] string paymentRefNumber)
        {
            return base.RegisterOfflinePayment(paymentRefNumber);
        }

        public override IActionResult PaymentStatusCallback([FromForm] MessageWrapper<PaymentStatus> message)
        {
            //logger.LogInfo($"Payment Status Changed ClientId: {message.ClientId} Data: {message.Data}");
            return base.PaymentStatusCallback(message);
        }

        //protected override void ActionExecuting(ActionExecutingContext context)
        //{
        //    context.SetCurrentPrincipal(true, "EGOV");
        //    base.ActionExecuting(context);
        //}
    }
}
