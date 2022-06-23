using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Web.Controllers
{
    [Route("e-applications")]
    [Authorize(Roles = RoleConstants.CentralAuth)]
    public class WApplicationsController : BaseApiCrudController<WApplicaitonDTO, WApplicaitonDTO, WApplicaitonGridDTO, WApplication, string>
    {
        private readonly IWApplicaitonService _wApplicaitonService;
        private readonly IApplicationWebService _applicationWebService;
        public WApplicationsController(IWApplicaitonService wApplicaitonService,
            IApplicationWebService applicationWebService) : base(wApplicaitonService)
        {
            _wApplicaitonService = wApplicaitonService;
            _applicationWebService = applicationWebService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<WApplicaitonGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._wApplicaitonService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpPut("{aId}/confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(string aId)
        {
            // todo: 
            //await this._applicationWebService.ConfirmPaymentAsync(aId);
            return Ok();
        }
    }
}
