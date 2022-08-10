using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Certificates;
using X.PagedList;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class CertificatesController : BaseController
    {
        private readonly CaisDbContext _caisDbContext;
        private readonly ILogger<CertificatesController> _logger;
        private readonly ICertificateService _certificateService;

        public CertificatesController(CaisDbContext caisDBContext, ILogger<CertificatesController> logger, ICertificateService certificateService)
        {
            _logger = logger;
            _caisDbContext = caisDBContext;
            _certificateService = certificateService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var userId = CurrentUserID;

            if (!string.IsNullOrEmpty(userId))
            {
                var v = await _certificateService.SelectPublicCertificates(userId);
                return View(v.ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View();
            }

        }
    }
}
