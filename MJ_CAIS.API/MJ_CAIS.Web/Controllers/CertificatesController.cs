using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("certificates")]
    [Authorize]
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
        public async Task<IActionResult> SaveSignerData(string aId, [FromBody] CertificateDTO aInDto)
        {
            await this._certificateService.SaveSignerDataAsync(aInDto);
            return Ok();
        }

        [HttpPut("{aId}/save-signer-data-by-judge")]
        public async Task<IActionResult> SaveSignerDataByJudge(string aId, [FromBody] CertificateDTO aInDto)
        {
            await this._certificateService.SaveSignerDataByJudgeAsync(aInDto);
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

        [HttpGet("{aId}/certificate-content-only")]
        public async Task<IActionResult> GetContentOnly(string aId)
        {
            var result = await this._certificateGenerationService.GetCertificateContentAsync(aId);
            if (result == null) return NotFound();

            var content = result;
            var fileName = "sertificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("by-application/{appId}")]
        public async Task<IActionResult> GetByApplication(string appId)
        {
            var result = await this._certificateService.GetByApplicationIdAsync(appId);
            return Ok(result);
        }

        [HttpGet("{appId}/bulletins-check/{onlyApproved}")]
        public async Task<IActionResult> GetBulletinsCheck(string appId)
        {
            var result = await this._certificateService.GetBulletinsCheckByIdAsync(appId);
            return Ok(result);
        }

        [HttpPut("{aId}/bulletins-selection")]
        public async Task<IActionResult> BulletinsSelection(string aId)
        {
            await this._certificateService.SetCertificateForSelectionAsync(aId);
            return Ok();
        }

        [HttpPut("{aId}/bulletins-rehabilitation")]
        public async Task<IActionResult> BulletinsRehabilitation(string aId, [FromBody] string[] ids)
        {
            await this._certificateService.SetBulletinsForRehabilitationAsync(aId, ids);
            return Ok();
        }
    }
}
