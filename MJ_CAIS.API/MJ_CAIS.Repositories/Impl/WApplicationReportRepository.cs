using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class WApplicationReportRepository : BaseAsyncRepository<WReport, CaisDbContext>, IWApplicationReportRepository
    {
        public WApplicationReportRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}