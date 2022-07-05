using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IInquiryRepository : IBaseAsyncRepository<VBulletin, string, CaisDbContext>
    {
        IQueryable<InquiryBulletinGridDTO> FilterBulletins(InquirySearchBulletinDTO searchParams);

        IQueryable<VBulletinsFull> FilterBulletinsForExport(InquirySearchBulletinDTO searchParams);

        IQueryable<InquiryBulletinByPersonGridDTO> FilterBulletinsByPerson(InquirySearchBulletinByPersonDTO searchParams);

        IQueryable<VBulletinsFull> FilterBulletinsByPersonDataForExport(InquirySearchBulletinByPersonDTO searchParams);

        Task<List<StatisticCountDTO>> GetStatisticForBulletinsAsync(StatisticSearchDTO searchParam);

        Task<StatisticCountDTO> GetStatisticForApplicationsAsync(StatisticSearchDTO searchParam);

        Task<StatisticCountDTO> GetStatisticForReportsAsync(StatisticSearchDTO searchParam);

        Task<StatisticCountDTO> GetStatisticForInternalRequestsAsync(StatisticSearchDTO searchParam);
    }
}
