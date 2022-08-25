using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins-administration")]
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.GlobalAdmin}")]
    public class BulletinsAdministrationController : BaseApiCrudController<BulletinAdministrationDTO, BulletinAdministrationDTO, BulletinAdministrationGridDTO, BBulletin, string>
    {
        private readonly IBulletinAdministrationService _bulletinAdministrationService;

        public BulletinsAdministrationController(IBulletinAdministrationService bulletinAdministrationService) : base(bulletinAdministrationService)
        {
            _bulletinAdministrationService = bulletinAdministrationService;
        }

        [HttpGet("")]
        public async Task<IActionResult> SearchBulletins(ODataQueryOptions<BulletinAdministrationGridDTO> aQueryOptions, [FromQuery]BulletinAdministrationSearchParamDTO searchParams)
        {
            var result = await this._bulletinAdministrationService.SelectAllWithPaginationAsync(aQueryOptions, searchParams);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPut("{aId}")]
        public async Task<IActionResult> Put(string aId, [FromBody] UnlockBulletinModelDTO aInDto)
        {
            await this._bulletinAdministrationService.UnlockBulletinAsync(aInDto);
            return Ok();
        }

        [HttpGet("{aId}/bulletin-statuses-history")]
        public IActionResult GetBulletinStatusByHistory(string aId)
        {
            var result = _bulletinAdministrationService.GetBulletinStatusesByHistory(aId);
            return Ok(result);
        }
    }
}
