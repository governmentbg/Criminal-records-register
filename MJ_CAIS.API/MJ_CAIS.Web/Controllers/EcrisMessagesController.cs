using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ecris-messages")]
    [Authorize(Roles = RoleConstants.CentralAuth)]
    public class EcrisMessagesController : BaseApiCrudController<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string>
    {
        private readonly IEcrisMessageService _ecrisMessageService;

        public EcrisMessagesController(IEcrisMessageService ecrisMessageService) 
            : base(ecrisMessageService)
        {
            _ecrisMessageService = ecrisMessageService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId)
        {
            var result = await this._ecrisMessageService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}/bulletins")]
        public async Task<IActionResult> GetEcrisBulletins(string aId)
        {
            var result = await this._ecrisMessageService.GetEcrisBulletinsByIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/fbbcs")]
        public async Task<IActionResult> GetEcrisFbbcs(string aId)
        {
            var result = await this._ecrisMessageService.GetEcrisFbbcsByIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        // TODO: remove, no adding
        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] EcrisMessageDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        // TODO: remove, no updating
        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] EcrisMessageDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }
    }
}
