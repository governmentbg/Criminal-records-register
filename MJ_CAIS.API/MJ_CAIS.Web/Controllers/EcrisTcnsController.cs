using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.EcrisTcn;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ecris-tcns")]
    [AllowAnonymous]
    //[Authorize(Roles = "CentralAuth")]
    public class EcrisTcnsController : BaseApiCrudController<EcrisTcnDTO, EcrisTcnDTO, EcrisTcnGridDTO, EEcrisTcn, string>
    {
        private readonly IEcrisTcnService _ecrisTcnService;

        public EcrisTcnsController(IEcrisTcnService ecrisTcnService) : base(ecrisTcnService)
        {
            _ecrisTcnService = ecrisTcnService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<EcrisTcnGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._ecrisTcnService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public new async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<EcrisTcnGridDTO> aQueryOptions)
        {
            return await base.GetAllNoWrap(aQueryOptions);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPut("{aId}/change-status/{statusId}")]
        public async Task<IActionResult> ChangeStatus(string aId, string statusId)
        {
            await this._ecrisTcnService.ChangeStatusAsync(aId, statusId);
            return Ok();
        }
    }
}
