using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class BulletinAdministrationService : BaseAsyncService<BulletinAdministrationDTO, BulletinAdministrationDTO, BulletinAdministrationGridDTO, BBulletin, string, CaisDbContext>, IBulletinAdministrationService
    {
        private readonly IBulletinAdministrationRepository _bulletinAdministrationRepository;

        public BulletinAdministrationService(IMapper mapper, IBulletinAdministrationRepository bulletinAdministrationRepository)
            : base(mapper, bulletinAdministrationRepository)
        {
            _bulletinAdministrationRepository = bulletinAdministrationRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
