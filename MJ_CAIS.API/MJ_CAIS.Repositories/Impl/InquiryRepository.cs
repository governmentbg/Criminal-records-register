using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Repositories.Impl;

namespace MJ_CAIS.Repositories
{
    public class InquiryRepository : BaseAsyncRepository<VBulletin, CaisDbContext>, IInquiryRepository
    {
        public InquiryRepository(CaisDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
