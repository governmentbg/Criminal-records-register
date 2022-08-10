using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Conviction;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [Authorize]
    public class ConvictionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICertificateService _certificateService;

        public ConvictionController(IMapper mapper, ICertificateService certificateService)
        {
            _mapper = mapper;
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<ActionResult> ViewByCode(string id)
        {
            //var cert = await _certificateService.GetCertificateDocumentContent(id);
            var cert = await _certificateService.GetWebCertificateByAccessCodeAsync(id);
            if (cert == null)
            {
                var empty = new ConvictionCodeDisplayModel
                {
                    IsEmptyResponse = true,
                    SearchCode = id,
                };
                return View(empty);
            }

            return File(cert.Content, cert.MimeType);
        }
    }
}
