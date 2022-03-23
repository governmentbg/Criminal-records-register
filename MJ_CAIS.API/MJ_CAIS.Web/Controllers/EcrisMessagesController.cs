using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ecris-messages")]
    [AllowAnonymous] // TODO: remove
    public class EcrisMessagesController : BaseApiCrudController<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string>
    {
        private readonly IEcrisMessageService _ecrisMessageService;

        public EcrisMessagesController(IEcrisMessageService ecrisMessageService) : base(ecrisMessageService)
        {
            _ecrisMessageService = ecrisMessageService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId)
        {
            var result = await this._ecrisMessageService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }
    }
}
