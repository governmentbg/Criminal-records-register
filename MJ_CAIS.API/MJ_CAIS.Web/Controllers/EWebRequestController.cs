using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EWebRequest;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("e-web-requests")]
    public class EWebRequestController : BaseApiCrudController<EWebRequestDTO, EWebRequestDTO, EWebRequestGridDTO, EWebRequest, string>
    {
        private readonly IEWebRequestsService _eWebRequestsService;

        public EWebRequestController(IEWebRequestsService eWebRequestsService) : base(eWebRequestsService)
        {
            _eWebRequestsService = eWebRequestsService;
        }


        [HttpGet("downloadHtml/{aId}")]
        public async Task<IActionResult> DownloadHtml(string aId)
        {
            var result = await this._eWebRequestsService.GetXmlTransformationById(aId);


            var fileName = "certificate.html";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(result, mimeType, fileName);
        }

    }
}
