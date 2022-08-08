using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess;
using MJ_CAIS.WebPortal.Public.Models.Certificates;
using X.PagedList;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    [Authorize]
    public class CertificatesController : BaseController
    {
        private readonly CaisDbContext _caisDbContext;
        private readonly ILogger<CertificatesController> _logger;
        //TODO: Create service, remove DBContext
        public CertificatesController(CaisDbContext caisDBContext, ILogger<CertificatesController> logger )
        {
            _logger = logger;
            _caisDbContext = caisDBContext;
        }

        public IActionResult Index(int? page)
        {
            var userId = CurrentUserID;

            if (!string.IsNullOrEmpty(userId))
            {
                var v = (from wa in _caisDbContext.WApplications
                         join c in _caisDbContext.WCertificates on wa.Id equals c.WApplId
                         where wa.UserCitizenId == userId
                         orderby c.ValidFrom
                         select new CeritificateViewModel (){
                             ValidFrom = c.ValidFrom,
                             WApplicationId = wa.Id,
                             AccessCode1 = c.AccessCode1,
                             Purpose = wa.PurposeNavigation.Name
                         }
                        );
                return View(v.ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View();
            }

        }
    }
}
