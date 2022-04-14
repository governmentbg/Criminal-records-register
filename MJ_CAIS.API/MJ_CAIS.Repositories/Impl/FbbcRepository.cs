using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class FbbcRepository : BaseAsyncRepository<Fbbc, CaisDbContext>, IFbbcRepository
    {
        public FbbcRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Fbbc> SelectAsync(string aId)
        {
            var fbbc = await _dbContext.Fbbcs
               .Include(x => x.BirthCountry)
               .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return fbbc;
        }
    }
}
