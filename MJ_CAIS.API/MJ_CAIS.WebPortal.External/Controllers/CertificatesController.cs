using AutoMapper;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Certificates;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [RedirectNotActive("Inactive", "Account")]
    [Authorize]
    public class CertificatesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICertificateService _certificateService;

        public CertificatesController(
            ICertificateService certificateService,
            IMapper mapper)
        {
            _mapper = mapper;
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var viewModel = new CertificateViewModel();
            return View(viewModel);
        }


        [HttpGet]
        [GridDataSourceAction]
        public async Task<ActionResult> GetCertificates()
        {
            var result = await _certificateService.SelectExternalCertificates(CurrentUserID);
            return View(
                result
                );
        }
    }
}
