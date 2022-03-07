using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins")]
    [AllowAnonymous] // TODO: remove
    public class BulletinsController : BaseApiCrudController<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string>
    {
        private readonly IBulletinService _bulletinService;

        public BulletinsController(IBulletinService bulletinService)
            : base(bulletinService)
        {
            _bulletinService = bulletinService;
        }

        [HttpGet("")]
        public new async Task<IActionResult> GetAll(ODataQueryOptions<BulletinGridDTO> aQueryOptions)
        {
            return await base.GetAll(aQueryOptions);
        }

        [HttpGet("getAll")]
        public new async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<BulletinGridDTO> aQueryOptions)
        {
            return await base.GetAllNoWrap(aQueryOptions);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] BulletinDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] BulletinDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }

        [HttpDelete("{aId}")]
        public new async Task<IActionResult> Delete(string aId)
        {
            return await base.Delete(aId);
        }

        [HttpGet("{aId}/offences")]
        public async Task<IActionResult> GetOffences(string aId)
        {
            var result = await this._bulletinService.GetOffencesByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/sanctions")]
        public async Task<IActionResult> GetSanctions(string aId)
        {
            var result = await this._bulletinService.GetSanctionsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/decisions")]
        public async Task<IActionResult> GetDecisions(string aId)
        {
            var result = await this._bulletinService.GetDecisionsByBulletinIdAsync(aId);
            return Ok(result);
        }
    }
}
