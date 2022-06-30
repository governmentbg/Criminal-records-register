using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IInquiryRepository : IBaseAsyncRepository<VBulletin, string, CaisDbContext>
    {
        IQueryable<InquiryBulletinGridDTO> FilterBulletins(InquirySearchBulletinDTO searchParams);
    }
}
