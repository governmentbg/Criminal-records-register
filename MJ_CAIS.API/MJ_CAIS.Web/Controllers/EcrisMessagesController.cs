using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;

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
        public new async Task<IActionResult> GetAll(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions)
        {
            return await base.GetAll(aQueryOptions);
        }
    }
}
