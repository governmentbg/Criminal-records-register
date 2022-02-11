using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins")]
    public class BulletinsController : BaseApiCrudController<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string>
    {
        private readonly IBulletinService _bulletinService;

        public BulletinsController(IBulletinService bulletinService) : base(bulletinService)
        {
            _bulletinService = bulletinService;
        }
    }
}
