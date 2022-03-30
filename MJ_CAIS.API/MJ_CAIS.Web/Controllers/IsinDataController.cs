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
    }
}
