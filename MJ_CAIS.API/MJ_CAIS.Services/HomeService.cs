using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class HomeService : IHomeService
    {
        private readonly IBulletinRepository _bulletinRepository;

        public HomeService(IBulletinRepository bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
        }

        public async Task<ObjectsCountDTO> GetCountAsync()
        {
            var bulletinStatusQuery = await _bulletinRepository.GetStatusCountAsync();
            var bulletinStatuses = await bulletinStatusQuery.ToListAsync();

            var result = new ObjectsCountDTO()
            {
                BulletinNewOfficeCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewOffice)?.Count ?? 0,
                BulletinNewEISSCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewEISS)?.Count ?? 0,
                BulletinForRehabilitationCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForRehabilitation)?.Count ?? 0,
                BulletinForDestructionCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForDestruction)?.Count ?? 0
            };

            return result;
        }
    }
}