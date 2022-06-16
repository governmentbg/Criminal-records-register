using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExtAdministration;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ext-administrations")]
    [Authorize(Roles = RoleConstants.GlobalAdmin)]
    public class ExtAdministrationController : BaseApiCrudController<ExtAdministrationDTO, ExtAdministrationDTO, ExtAdministrationGridDTO, GExtAdministration, string>
    {
        private readonly IExtAdministrationService _extAdministrationService;

        public ExtAdministrationController(IExtAdministrationService extAdministrationService) : base(extAdministrationService)
        {
            _extAdministrationService = extAdministrationService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<ExtAdministrationGridDTO> aQueryOptions)
        {
            var result = await this._extAdministrationService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllNoWrap()
        {
            var result = await this._extAdministrationService.SelectAllAsync();
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            var result = await this._extAdministrationService.SelectAsync(aId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] ExtAdministrationDTO aInDto)
        {
            var id = await this._extAdministrationService.InsertAsync(aInDto);
            return Ok(new { id });
        }

        [HttpPut("{aId}")]
        public async Task<IActionResult> Put(string aId, [FromBody] ExtAdministrationDTO aInDto)
        {
            await this._extAdministrationService.UpdateAsync(aId, aInDto);
            return Ok();
        }
    }
}
