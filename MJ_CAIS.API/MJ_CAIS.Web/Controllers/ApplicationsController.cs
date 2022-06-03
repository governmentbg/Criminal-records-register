using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace MJ_CAIS.Web.Controllers
{
    [Route("applications")]
    [AllowAnonymous]
    public class ApplicationsController : BaseApiCrudController<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, AApplication, string>
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService) : base(applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._applicationService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public virtual async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPost("")]
        public  virtual async Task<IActionResult> Post([FromBody] ApplicationInDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("finalEdit")]
        public virtual async Task<IActionResult> FinalEdit([FromBody] ApplicationInDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("{aId}")]
        public virtual async Task<IActionResult> Put(string aId, [FromBody] ApplicationInDTO aInDto)
        {
            await this._applicationService.UpdateAsync(aId, aInDto);
            return Ok();
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._applicationService.GetDocumentsByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpPost("{aId}/documents")]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] ApplicationDocumentDTO aInDto)
        {
            await this._applicationService.InsertApplicationDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._applicationService.DeleteDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
        public async Task<IActionResult> GetContents(string documentId)
        {
            var result = await this._applicationService.GetDocumentContentAsync(documentId);
            if (result == null) return NotFound();

            var content = result.DocumentContent;
            var fileName = result.Name;
            var mimeType = getContentType(fileName);

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("{aId}/person-alias")]
        public async Task<IActionResult> GetPersonAlias(string aId)
        {
            var result = await this._applicationService.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/application-history")]
        public async Task<IActionResult> GetAppplicationHistory(string aId)
        {
            var result = await this._applicationService.SelectApplicationPersStatusHAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/application-certificate")]
        public async Task<IActionResult> GetAppplicationCertificate(string aId)
        {
            var result = await this._applicationService.SelectApplicationCertificateByApplicationIdAsync(aId);
            return Ok(result);
        }
      
        private string getContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
