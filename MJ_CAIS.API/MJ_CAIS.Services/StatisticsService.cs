using MJ_CAIS.DTO.Statistics;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IBulletinRepository _bulletinRepository;

        public StatisticsService(IBulletinRepository bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
        }

        public async Task<List<StatisticsCountDTO>> GetStatisticsForBulletinsAsync(StatisticsSearchDTO searchParams)
           => await _bulletinRepository.GetStatisticsForBulletinsAsync(searchParams);

        public async Task<List<StatisticsCountDTO>> GetStatisticsForApplicationsAsync(StatisticsSearchDTO searchParams)
            => await _bulletinRepository.GetStatisticsForApplicationsAsync(searchParams);
    }
}