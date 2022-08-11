using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.Repositories.Impl
{
    public class ReportSearchPersonsRepository : BaseAsyncRepository<WReportSearchPer, CaisDbContext>, IReportSearchPersonsRepository
    {
        public ReportSearchPersonsRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
