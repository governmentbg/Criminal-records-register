using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class ReportApplicationRepository : BaseAsyncRepository<AReportApplication, CaisDbContext>, IReportApplicationRepository
    {
        public ReportApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<AReportApplication> SelectAsync(string id)
        {
            var result = await this._dbContext.AReportApplications
                .Include(x => x.ARepCitizenships)
                .Include(x => x.BirthCountry)
                .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
