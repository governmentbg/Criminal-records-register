using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("e-applications")]
    [Authorize(Roles = RoleConstants.CentralAuth)]
    public class WApplicationsController : BaseApiCrudController<WApplicaitonDTO, WApplicaitonDTO, WApplicaitonGridDTO, WApplication, string>
    {
        private readonly IWApplicationService _wApplicaitonService;
        private readonly IDocumentService _documentService;
        public WApplicationsController(IWApplicationService wApplicaitonService, IDocumentService documentService)
            : base(wApplicaitonService)
        {
            _wApplicaitonService = wApplicaitonService;
            _documentService = documentService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<WApplicaitonGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._wApplicaitonService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public virtual async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("{aId}/person-alias")]
        public async Task<IActionResult> GetPersonAlias(string aId)
        {
            var result = await this._wApplicaitonService.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._documentService.GetDocumentsByApplicationIdAsync(aId);
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

        [HttpGet("{aId}/e-application-history")]
        //[Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetApplicationHistory(string aId)
        {
            var result = await this._wApplicaitonService.SelectApplicationPersStatusHAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/eWeb-requests")]
        //[Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetEWebRequestsByApplicationId(string aId)
        {
            var result = await this._wApplicaitonService.SelectAllEWebRequestsByApplicationIdAsync(aId);
            return Ok(result);
        }
    }
}
