using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface ISearchReportApplicationRepository : IBaseAsyncRepository<AReport, string, CaisDbContext>
    {
        Task<IQueryable<SearchReportApplicationGridDTO>> SelectAllAsync();
    }
}
