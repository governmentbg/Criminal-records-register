using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("help")]
    [Authorize]
    public class HelpController : BaseApiController
    {
        [HttpGet("cbs")]
        public async Task<IActionResult> GetContentsCbs()
        {
            var fileName = "MJ_CAIS_SUM_CBS.docx";
            return await ReturnFileAsync(fileName);
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetContentsEmployee()
        {
            var fileName = "MJ_CAIS_SUM_Employee.docx";
            return await ReturnFileAsync(fileName);
        }

        [HttpGet("administration")]
        public async Task<IActionResult> GetContentsAdministration()
        {
            var fileName = "MJ_CAIS_SАM_Administration.docx";
            return await ReturnFileAsync(fileName);
        }

        private async Task<IActionResult> ReturnFileAsync(string fileName)
        {
            var filePath = Path.Combine("HelpFiles", fileName);
            var byteArr = await System.IO.File.ReadAllBytesAsync(filePath);
            var mimeType = GetContentType(fileName);

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(byteArr, mimeType, fileName);
        }

        private string GetContentType(string fileName)
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
