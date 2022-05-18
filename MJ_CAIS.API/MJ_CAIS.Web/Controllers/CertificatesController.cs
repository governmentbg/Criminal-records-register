using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("certificates")]
    public class CertificatesController : BaseApiCrudController<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string>
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService) : base(certificateService)
        {
            _certificateService = certificateService;
        }
    }
}
