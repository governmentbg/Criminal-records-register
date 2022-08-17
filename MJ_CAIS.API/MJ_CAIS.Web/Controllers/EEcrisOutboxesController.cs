using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisOutbox;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ecris-outboxes")]
    public class EEcrisOutboxesController : BaseApiCrudController<EcrisOutboxDTO, EcrisOutboxDTO, EcrisOutboxGridDTO, EEcrisOutbox, string>
    {
        private readonly IEcrisOutboxService _ecrisOutboxService;
        private readonly IRequestService _requestService;

        public EEcrisOutboxesController(IEcrisOutboxService ecrisOutboxService, IRequestService requestService) : base(ecrisOutboxService)
        {
            _ecrisOutboxService = ecrisOutboxService;
            _requestService = requestService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<EcrisOutboxGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._ecrisOutboxService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}/xml")]
        public async Task<IActionResult> GetContentOnly(string aId)
        {
            var xml = await this._ecrisOutboxService.GetXmlContentAsync(aId);
            var fileNameXml = "ecris-outbox-msg.xml";
            var mimeTypeXml = "application/octet-stream";

            Response.Headers.Add("File-Name", fileNameXml);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(xml, mimeTypeXml, fileNameXml);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPut("resend/{ecrisMsgId}")]
        public async Task<IActionResult> Resend(string ecrisMsgId)
        {
            await this._requestService.RecreateResponseToRequest(ecrisMsgId);
            return Ok();
        }
    }
}
