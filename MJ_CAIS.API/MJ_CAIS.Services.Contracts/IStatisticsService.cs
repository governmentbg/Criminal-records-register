using MJ_CAIS.DTO.Statistics;

namespace MJ_CAIS.Services.Contracts
{
    public interface IStatisticsService
    {
        Task<List<StatisticsCountDTO>> GetStatisticsForBulletinsAsync(StatisticsSearchDTO searchParams);
    }
}
