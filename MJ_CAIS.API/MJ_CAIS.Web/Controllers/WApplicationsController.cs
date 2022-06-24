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
        private readonly IWApplicationService _wApplicaitonService;
        public WApplicationsController(IWApplicationService wApplicaitonService) 
            : base(wApplicaitonService)
        {
            _wApplicaitonService = wApplicaitonService;
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
            await this._wApplicaitonService.ConfirmPaymentAsync(aId);
            return Ok();
        }

        [HttpPut("{aId}/process-tax-free/{approve}")]
        public async Task<IActionResult> ProcessTaxFree(string aId, bool approve)
        {
            await this._wApplicaitonService.ProcessTaxFreeAsync(aId, approve);
            return Ok();
        }
    }
}
