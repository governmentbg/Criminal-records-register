using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Repositories.Impl
{
    public class IsinDataRepository : BaseAsyncRepository<EIsinDatum, CaisDbContext>, IIsinDataRepository
    {
        public IsinDataRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.EIsinData.AsNoTracking()
                .Where(x => x.Status == IsinDataConstants.Status.New || x.Status == IsinDataConstants.Status.Identified)
                .GroupBy(x => x.Status)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }
    }
}
