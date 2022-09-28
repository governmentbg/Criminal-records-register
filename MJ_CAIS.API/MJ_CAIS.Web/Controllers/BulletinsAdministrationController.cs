using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins-administration")]
    [Authorize(Roles = $"{RoleConstants.Supervisor}")]
    public class BulletinsAdministrationController : BaseApiCrudController<BulletinAdministrationDTO, BulletinAdministrationDTO, BulletinAdministrationGridDTO, BBulletin, string>
    {
        private readonly IBulletinAdministrationService _bulletinAdministrationService;
        private readonly IBulletinService _bulletinService;

        public BulletinsAdministrationController(IBulletinAdministrationService bulletinAdministrationService,
            IBulletinService bulletinService)
            : base(bulletinAdministrationService)
        {
            _bulletinAdministrationService = bulletinAdministrationService;
            _bulletinService = bulletinService;
        }

        [HttpGet("")]
        public async Task<IActionResult> SearchBulletins(ODataQueryOptions<BulletinAdministrationGridDTO> aQueryOptions, [FromQuery] BulletinAdministrationSearchParamDTO searchParams)
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

        [HttpPut("{aId}/delete")]
        public async Task<IActionResult> Delete(string aId, [FromBody] DeleteBulletinDTO aInDto)
        {
            await this._bulletinService.DeleteBulletinByIdAsync(aInDto.Id, aInDto.Description);
            return Ok();
        }

        [HttpGet("{aId}/bulletin-statuses-history")]
        public IActionResult GetBulletinStatusByHistory(string aId)
        {
            var result = _bulletinAdministrationService.GetBulletinStatusesByHistory(aId).ToList();
            if (!result.Any(x => x.Id == BulletinConstants.Status.NewOffice))
            {
                if (result == null) result = new List<DTO.Nomenclature.BaseNomenclatureDTO>();
                result.Add(new DTO.Nomenclature.BaseNomenclatureDTO
                {
                    Id = BulletinConstants.Status.NewOffice,
                    Code = BulletinConstants.Status.NewOffice,
                    Name = BulletinResources.nameNewOffice
                });
            }

            return Ok(result);
        }
    }
}
