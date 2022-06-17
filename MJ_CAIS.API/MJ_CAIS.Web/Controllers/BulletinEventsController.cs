using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletin-events")]
    [Authorize(Roles = $"{RoleConstants.Judge},{RoleConstants.CentralAuth},{RoleConstants.Normal}")]
    public class BulletinEventsController : BaseApiCrudController<BulletinEventDTO, BulletinEventDTO, BulletinEventGridDTO, BBulEvent, string>
    {
        private readonly IBulletinEventService _bulletinEventService;

        public BulletinEventsController(IBulletinEventService bulletinEventService) : base(bulletinEventService)
        {
            _bulletinEventService = bulletinEventService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<BulletinEventGridDTO> aQueryOptions, string groupCode, string? statusId, string? bulletinId)
        {
            var result = await this._bulletinEventService.SelectAllWithPaginationAsync(aQueryOptions, groupCode, statusId,bulletinId);
            return Ok(result);
        }

        [HttpPut("{aId}/change-status/{statusId}")]
        public async Task<IActionResult> ChangeStatus(string aId, string statusId)
        {
            await this._bulletinEventService.ChangeStatusAsync(aId, statusId);
            return Ok();
        }
    }
}
