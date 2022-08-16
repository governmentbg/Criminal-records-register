using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.EcrisInbox;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ecris-inboxes")]
    [Authorize(Roles = $"{RoleConstants.CentralAuth}")]
    public class EcrisInboxesController : BaseApiCrudController<EcrisInboxDTO, EcrisInboxDTO, EcrisInboxGridDTO, EEcrisInbox, string>
    {
        private readonly IEcrisInboxService _ecrisInboxService;

        public EcrisInboxesController(IEcrisInboxService ecrisInboxService) : base(ecrisInboxService)
        {
            _ecrisInboxService = ecrisInboxService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<EcrisInboxGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._ecrisInboxService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}/xml/{traits}")]
        public async Task<IActionResult> GetContentOnly(string aId, bool traits)
        {
            var xml = await this._ecrisInboxService.GetXmlContentAsync(aId, traits);
            var fileNameXml = "ecris-inbox-msg.xml";
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
    }
}
