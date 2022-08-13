using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Web.Controllers
{
    [Route("internal-requests")]
    [Authorize(Roles = $"{RoleConstants.Judge},{RoleConstants.CentralAuth}")]
    public class InternalRequestsController : BaseApiCrudController<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, NInternalRequest, string>
    {
        private readonly IInternalRequestService _internalRequestService;

        public InternalRequestsController(IInternalRequestService internalRequestService) : base(internalRequestService)
        {
            _internalRequestService = internalRequestService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string statuses, bool fromAuth)
        {
            var result = await this._internalRequestService.SelectAllWithPaginationAsync(aQueryOptions, statuses, fromAuth);
            return Ok(result);
        }

        [HttpGet("requests-count")]
        public async Task<IActionResult> GetRequestCount()
        {
            var result = await this._internalRequestService.GetInternalRequestsCount();
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("selected-bulletins/{aId}")]
        public IActionResult GetSelectedBulletins(string aId)
        {
            var result = this._internalRequestService.GetSelectedBulletins(aId);
            return Ok(result);
        }

        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] InternalRequestDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] InternalRequestDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }

        [HttpDelete("{aId}")]
        public new async Task<IActionResult> Delete(string aId)
        {
            await this.baseService.DeleteAsync(aId);
            return Ok();
        }

        [HttpPut("{aId}/change-status/{statusId}")]
        public async Task<IActionResult> ChangeStatus(string aId, string statusId)
        {
            await this._internalRequestService.ChangeStatusAsync(aId, statusId);
            return Ok();
        }

        [HttpPut("{aId}/replay")]
        public async Task<IActionResult> ChangeStatus(string aId, [FromBody] InternalRequstReplayDTO aInDto)
        {
            await this._internalRequestService.ReplayAsync(aId, aInDto.Accepted, aInDto.ResponseDescr);
            return Ok();
        }

        [HttpPut("mark-as-read")]
        public async Task<IActionResult> MarkAsRead([FromBody] List<string> ids)
        {
            await this._internalRequestService.MarkAsReaded(ids);
            return Ok();
        }

        [HttpGet("get-pids-for-selection-dialog")]
        public async Task<IActionResult> GetAllPidsForSelection(ODataQueryOptions<SelectPidGridDTO> aQueryOptions)
        {
            var result = await this._internalRequestService.SelectAllPidsForSelectionWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("person-bulletins/{personId}")]
        public IActionResult GetPersonBulletins(string personId)
        {
            var result = this._internalRequestService.GetPersonBulletins(personId);
            return Ok(result);
        }
    }
}