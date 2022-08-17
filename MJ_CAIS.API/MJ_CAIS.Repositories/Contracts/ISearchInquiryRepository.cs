using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface ISearchInquiryRepository : IBaseAsyncRepository<AReport, string, CaisDbContext>
    {
        Task<IQueryable<SearchInquiryGridDTO>> SelectAllAsync();
        Task<SearchInquiryDTO> SelectByIdAsync(string aId);
    }
}
