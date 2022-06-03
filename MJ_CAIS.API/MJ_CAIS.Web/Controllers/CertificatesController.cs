using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using MJ_CAIS.ExternalWebServices.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace MJ_CAIS.Web.Controllers
{
    [Route("certificates")]
    [AllowAnonymous]
    public class CertificatesController : BaseApiCrudController<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string>
    {
        private readonly ICertificateService _certificateService;
        private readonly ICertificateGenerationService _certificateGenerationService;

        public CertificatesController(ICertificateService certificateService, ICertificateGenerationService certificateGenerationService)
            : base(certificateService)
        {
            _certificateService = certificateService;
            _certificateGenerationService = certificateGenerationService;
        }

        [HttpPut("{aId}/save-signer-data")]
        public async Task<IActionResult> SaveSignerData(string aId, [FromBody] CertificateSignerDTO aInDto)
        {
            await this._certificateService.SaveSignerDataAsync(aInDto);
            return Ok();
        }

        [HttpGet("{aId}/certificate-content")]
        public async Task<IActionResult> GetContent(string aId)
        {
            var result = await this._certificateGenerationService.CreateCertificate(aId);
            if (result == null) return NotFound();

            var content = result;
            var fileName = "sertificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }
    }
}
