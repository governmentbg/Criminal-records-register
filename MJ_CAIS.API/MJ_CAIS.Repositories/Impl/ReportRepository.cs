using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class ReportRepository : BaseAsyncRepository<AReport, CaisDbContext>, IReportRepository
    {
        public ReportRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
