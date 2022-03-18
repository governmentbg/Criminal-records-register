using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.StaticFiles;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins")]
    [AllowAnonymous] // TODO: remove
    public class BulletinsController : BaseApiCrudController<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string>
    {
        private readonly IBulletinService _bulletinService;

        public BulletinsController(IBulletinService bulletinService)
            : base(bulletinService)
        {
            _bulletinService = bulletinService;
        }

        [HttpGet("")]
        public new async Task<IActionResult> GetAll(ODataQueryOptions<BulletinGridDTO> aQueryOptions,string statusId)
        {
            var result = await _bulletinService.GetAllCustomAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public new async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<BulletinGridDTO> aQueryOptions)
        {
            return await base.GetAllNoWrap(aQueryOptions);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] BulletinDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] BulletinDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }

        [HttpDelete("{aId}")]
        public new async Task<IActionResult> Delete(string aId)
        {
            return await base.Delete(aId);
        }

        [HttpGet("{aId}/offences")]
        public async Task<IActionResult> GetOffences(string aId)
        {
            var result = await this._bulletinService.GetOffencesByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/sanctions")]
        public async Task<IActionResult> GetSanctions(string aId)
        {
            var result = await this._bulletinService.GetSanctionsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/decisions")]
        public async Task<IActionResult> GetDecisions(string aId)
        {
            var result = await this._bulletinService.GetDecisionsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._bulletinService.GetDocumentsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpPost("{aId}/documents")]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] DocumentDTO aInDto)
        {
            await this._bulletinService.InsertBulletinDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._bulletinService.DeleteComplaintDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
        public async Task<IActionResult> GetContents(string documentId)
        {
            var result = await this._bulletinService.GetDocumentContentAsync(documentId);
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
            var result = await this._bulletinService.GetPersonAliasByBulletinIdAsync(aId);
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
