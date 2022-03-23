using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace MJ_CAIS.Web.Controllers
{
    [Route("fbbcs")]
    [AllowAnonymous]
    public class FbbcsController : BaseApiCrudController<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string>
    {
        private readonly IFbbcService _fbbcService;

        public FbbcsController(IFbbcService fbbcService) : base(fbbcService)
        {
            _fbbcService = fbbcService;
        }

        [HttpGet("")]
        public new async Task<IActionResult> GetAll(ODataQueryOptions<FbbcGridDTO> aQueryOptions)
        {
            return await base.GetAll(aQueryOptions);
        }

        [HttpGet("getAll")]
        public new async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<FbbcGridDTO> aQueryOptions)
        {
            return await base.GetAllNoWrap(aQueryOptions);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] FbbcDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] FbbcDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }

        [HttpDelete("{aId}")]
        public new async Task<IActionResult> Delete(string aId)
        {
            return await base.Delete(aId);
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._fbbcService.GetDocumentsByFbbcIdAsync(aId);
            return Ok(result);
        }


        [HttpPost("{aId}/documents")]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] FbbcDocumentDTO aInDto)
        {
            await this._fbbcService.InsertFbbcDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._fbbcService.DeleteDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
        public async Task<IActionResult> GetContents(string documentId)
        {
            var result = await this._fbbcService.GetDocumentContentAsync(documentId);
            if (result == null) return NotFound();

            var content = result.DocumentContent;
            var fileName = result.Name;
            var mimeType = getContentType(fileName);

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
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