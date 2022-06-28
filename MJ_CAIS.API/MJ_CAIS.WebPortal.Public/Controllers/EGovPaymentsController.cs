using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Dynamic;
using TL.EGovPayments;
using TL.EGovPayments.ControllerModels;
using TL.EGovPayments.Interfaces;
using TL.EGovPayments.JsonEnums;
using TL.EGovPayments.JsonModels;



namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    internal class DynamicViewData : DynamicObject
    {
        private readonly Func<ViewDataDictionary> _viewDataFunc;

        public DynamicViewData(Func<ViewDataDictionary> viewDataFunc)
        {
            if (viewDataFunc == null)
            {
                throw new ArgumentNullException(nameof(viewDataFunc));
            }

            _viewDataFunc = viewDataFunc;
        }

        private ViewDataDictionary ViewData
        {
            get
            {
                var viewData = _viewDataFunc();
                if (viewData == null)
                {
                    throw new InvalidOperationException("ViewData is null");
                }

                return viewData;
            }
        }

        // Implementing this function extends the ViewBag contract, supporting or improving some scenarios. For example
        // having this method improves the debugging experience as it provides the debugger with the list of all
        // properties currently defined on the object.
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return ViewData.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            result = ViewData[binder.Name];

            // ViewDataDictionary[key] will never throw a KeyNotFoundException.
            // Similarly, return true so caller does not throw.
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            ViewData[binder.Name] = value;

            // Can always add / update a ViewDataDictionary value.
            return true;
        }
    }
}

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

        private DynamicViewData _viewBag;
        private ViewDataDictionary _viewData;


        [ViewDataDictionary]
        public ViewDataDictionary ViewData
        {
            get
            {
                if (_viewData == null)
                {
                    // This should run only for the controller unit test scenarios
                    _viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), ControllerContext.ModelState);
                }

                return _viewData;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Argument {0} cannot be null", nameof(ViewData));
                }

                _viewData = value;
            }
        }

        /// <summary>
        /// Gets the dynamic view bag.
        /// </summary>
        public dynamic ViewBag
        {
            get
            {
                if (_viewBag == null)
                {
                    _viewBag = new DynamicViewData(() => ViewData);
                }

                return _viewBag;
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
            try
            {
                EGovPaymentRequestModel paymentRequest = JsonConvert.DeserializeObject<EGovPaymentRequestModel>(TempData["paymentRequestModel"].ToString());
                //var res = await base.CreateVPOSPayment(paymentRequest);
                var res = await base.CreateVPOSPayment(paymentRequest);
                return RedirectToAction("Index", "Application");

                //var objectResult = res as ObjectResult;
                //dynamic value = objectResult.Value;

                //ViewBag.postUrl = value.PostUrl;
                //ViewBag.ClientId = value.Message.ClientId;
                //ViewBag.Data = value.Message.Data;
                //ViewBag.HMAC = value.Message.HMAC;

                //return new ViewResult()
                //{
                //    ViewName = "/Views/Application/PayEgov.cshtml",
                //    ViewData = ViewData,
                //    TempData = TempData
                //};
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //return View("Index", "Application");
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

        [HttpPost]
        [AllowAnonymous]
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
