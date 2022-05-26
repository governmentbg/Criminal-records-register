using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationRepository : BaseAsyncRepository<AApplication, CaisDbContext>, IApplicationRepository
    {
        public ApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.AApplications.AsNoTracking()
                .Where(x => x.StatusCode == ApplicationConstants.ApplicationStatuses.NewId ||
                            x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckPayment ||
                            x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckTaxFree ||
                            x.StatusCode == ApplicationConstants.ApplicationStatuses.BulletinsCheck)
                .GroupBy(x => x.StatusCode)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }
    }
}
