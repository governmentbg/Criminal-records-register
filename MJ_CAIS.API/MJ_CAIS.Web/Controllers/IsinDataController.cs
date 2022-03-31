using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("isin-data")]
    [AllowAnonymous] // TODO: remove
    public class IsinDataController : BaseApiCrudController<IsinDataDTO, IsinDataDTO, IsinDataGridDTO, EIsinDatum, string>
    {
        private readonly IIsinDataService _isinDataService;

        public IsinDataController(IIsinDataService isinDataService) : base(isinDataService)
        {
            _isinDataService = isinDataService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string status)
        {
            var result = await this._isinDataService.SelectAllWithPaginationAsync(aQueryOptions, status);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("bulletins")]
        public async Task<IActionResult> GetAllBulletins(ODataQueryOptions<IsinBulletinGridDTO> aQueryOptions)
        {
            var result = await this._isinDataService.SelectIsinBulletinAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpPost("{aId}/select-bulletin/{bulletinId}")]
        public async Task<IActionResult> SelectBulletin(string aId, string bulletinId)
        {
            await this._isinDataService.SelectBulletinAsync(aId, bulletinId);
            return Ok();
        }
    }
}
