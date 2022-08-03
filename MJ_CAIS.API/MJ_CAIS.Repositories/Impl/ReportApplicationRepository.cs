using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class ReportApplicationRepository : BaseAsyncRepository<AReportApplication, CaisDbContext>, IReportApplicationRepository
    {
        public ReportApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
