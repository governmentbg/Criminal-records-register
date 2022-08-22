using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WCertificate;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("w-certificates")]
    public class WCertificatesController : BaseApiCrudController<WCertificateDTO, WCertificateDTO, WCertificateGridDTO, WCertificate, string>
    {
        private readonly IWCertificateService _wCertificateService;

        public WCertificatesController(IWCertificateService wCertificateService) : base(wCertificateService)
        {
            _wCertificateService = wCertificateService;
        }

        [HttpGet("by-application/{appId}")]
        public async Task<IActionResult> GetByApplication(string appId)
        {
            var result = await this._wCertificateService.GetByApplicationIdAsync(appId);
            return Ok(result);
        }

        [HttpGet("content-by-application/{appId}")]
        public async Task<IActionResult> GetContentByApp(string appId)
        {
            var result = await this._wCertificateService.GetContentByApplicationIdAsync(appId);
            var content = result;
            var fileName = "certificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }
    }
}
