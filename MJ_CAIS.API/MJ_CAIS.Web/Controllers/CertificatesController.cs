using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.ExternalWebServices;
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
        private readonly ICertificateValidatorService _certificateValidatorService;
        private readonly ICriminalRecordsReportService _criminalRecordsReportService;

        public CertificatesController(ICertificateService certificateService, ICertificateGenerationService certificateGenerationService, ICertificateValidatorService certificateValidatorService, ICriminalRecordsReportService criminalRecordsReportService)
            : base(certificateService)
        {
            _certificateService = certificateService;
            _certificateGenerationService = certificateGenerationService;
            _certificateValidatorService = certificateValidatorService;
            _criminalRecordsReportService = criminalRecordsReportService;
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
            var fileName = "certificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("{aId}/certificate-content-only/{applicationType}/{statusCode}")]
        public async Task<IActionResult> GetContentOnly(string aId, string applicationType, string statusCode)
        {

            if (applicationType == ApplicationConstants.ApplicationTypes.ApplicationRequestOld ||
                applicationType == ApplicationConstants.ApplicationTypes.ConvictionRequestOld)
            {
                var resultXML = await this._certificateGenerationService.GetCertificateContentAsync(aId);
                var fileNameXML = "certificate.html";
                var mimeTypeXML = "application/octet-stream";

                Response.Headers.Add("File-Name", fileNameXML);
                Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

                return File(resultXML, mimeTypeXML, fileNameXML);
            }

            byte[] result;
            if (CertificateConstants.CertificateStatues.CertificateUserSign != statusCode)
            {
                result = await this._certificateValidatorService.GetUnsignedPdfForDownload(aId);
            }
            else
            {
                result = await this._certificateValidatorService.GetPdfForDownload(aId);
            }

            var content = result;
            var fileName = "certificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("archive-content/{aId}")]
        public async Task<IActionResult> GetArchivedDocumentContentOnly(string aId)
        {
            var result = await this._certificateGenerationService.GetArchiveDocumentContentAsync(aId);
            if (result is null) return NotFound();

            var fileNameXML = "archived-document.html";
            var mimeTypeXML = "application/octet-stream";

            Response.Headers.Add("File-Name", fileNameXML);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(result, mimeTypeXML, fileNameXML);
        }

        [HttpPost("{certId}/uploadSignedCertificate")]
        public async Task<IActionResult> UploadSignedCertificate(string certId, [FromBody] CertificateDocumentDTO aInDto)
        {
            var isValid = await _certificateValidatorService.ValidatePdf(aInDto.DocumentContent, certId);
            if (isValid)
            {
                await this._certificateService.UploadSignedDocumet(certId, aInDto);
              
            }
            return Ok();
        }

        [HttpGet("updateCertificateStatus/{certId}")]
        public async Task<IActionResult> UpdateCertificateStatus(string certId)
        {
            await this._certificateService.UpdateCertificateStatus(certId);
            return Ok();
        }

        [HttpGet("by-application/{appId}")]
        public async Task<IActionResult> GetByApplication(string appId)
        {
            var result = await this._certificateService.GetByApplicationIdAsync(appId);
            return Ok(result);
        }

        [HttpGet("set-status-to-delivered/{appId}")]
        public async Task<IActionResult> SetStatusToDelivered(string appId)
        {
            await this._certificateService.SetStatusToDelivered(appId);
            return Ok();
        }

        [HttpGet("set-status-to-canceled/{appId}")]
        public async Task<IActionResult> SetStatusToCanceled(string appId)
        {
            await this._certificateService.SetStatusToCanceled(appId);
            return Ok();
        }

        [HttpGet("by-application-canceled/{appId}")]
        public async Task<IActionResult> GetCanceledByApplication(string appId)
        {
            var result = await this._certificateService.GetCanceledByApplicationIdAsync(appId);
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

        [HttpPut("{aId}/bulletins-rehabilitation/{newRequestId}")]
        public async Task<IActionResult> BulletinsRehabilitation(string aId, string newRequestId, [FromBody] string[] ids)
        {
            await this._certificateService.SetBulletinsForRehabilitationAsync(aId, newRequestId, ids);
            return Ok();
        }

        [HttpGet("html-report/{idType}/{id}")]
        public async Task<IActionResult> GetHtmlReport(string idType, string id)
        {
            var result = await _criminalRecordsReportService.GetCriminalRecordsReportHTMLAsync(
                new DTO.ExternalServicesHost.CriminalRecordsExtendedRequestType()
                {
                    CriminalRecordsRequest = new DTO.ExternalServicesHost.CriminalRecordsRequestType()
                    {
                        IdentifierTypeSpecified = true,
                        IdentifierType = Enum.Parse<DTO.ExternalServicesHost.IdentifierType>(idType),
                        PID = id
                    }
                });
            return Content(result, "text/html");
        }
    }
}
