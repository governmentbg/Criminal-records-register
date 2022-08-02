using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class WApplicationReportSearchPerRepository : BaseAsyncRepository<WReportSearchPer, CaisDbContext>, IWApplicationReportSearchPerRepository
    {
        public WApplicationReportSearchPerRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}