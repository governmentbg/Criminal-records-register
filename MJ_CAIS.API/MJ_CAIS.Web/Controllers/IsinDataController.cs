using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("isin-data")]
    [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.CentralAuth},{RoleConstants.Judge}")]
    public class IsinDataController : BaseApiCrudController<IsinDataDTO, IsinDataDTO, IsinDataGridDTO, EIsinDatum, string>
    {
        private readonly IIsinDataService _isinDataService;

        public IsinDataController(IIsinDataService isinDataService) : base(isinDataService)
        {
            _isinDataService = isinDataService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string? status, string? bulletinId)
        {
            var result = await this._isinDataService.SelectAllWithPaginationAsync(aQueryOptions, status, bulletinId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("preview/{aId}")]
        public async Task<IActionResult> GetForPreview(string aId)
        {
            var result = await this._isinDataService.SelectForPreviewAsync(aId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("bulletins")]
        [Authorize(Roles = $"{RoleConstants.CentralAuth}")]
        public async Task<IActionResult> GetAllBulletins(ODataQueryOptions<IsinBulletinGridDTO> aQueryOptions)
        {
            var result = await this._isinDataService.SelectIsinBulletinAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpPost("bulletins/{aId}/select/{bulletinId}")]
        [Authorize(Roles = $"{RoleConstants.CentralAuth}")]
        public async Task<IActionResult> SelectBulletin(string aId, string bulletinId)
        {
            await this._isinDataService.SelectBulletinAsync(aId, bulletinId);
            return Ok();
        }

        [HttpPost("{aId}/close")]
        public async Task<IActionResult> Close(string aId)
        {
            await this._isinDataService.CloseAsync(aId);
            return Ok();
        }
    }
}
