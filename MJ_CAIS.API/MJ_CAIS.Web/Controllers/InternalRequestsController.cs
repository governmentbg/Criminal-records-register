using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Web.Controllers
{
    [Route("internal-requests")]
    [AllowAnonymous] // TODO: remove
    public class InternalRequestsController : BaseApiCrudController<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, BInternalRequest, string>
    {
        private readonly IInternalRequestService _internalRequestService;

        public InternalRequestsController(IInternalRequestService internalRequestService) : base(internalRequestService)
        {
            _internalRequestService = internalRequestService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string? bulletinId)
        {
            var result = await this._internalRequestService.SelectAllWithPaginationAsync(aQueryOptions, bulletinId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
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
    }
}
