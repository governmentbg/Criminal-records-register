using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class EWebRequestsRepository : BaseAsyncRepository<EWebRequest, CaisDbContext>, IEWebRequestsRepository
    {
        public EWebRequestsRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }


        public virtual async Task<EWebRequest> SelectAsync(string id)
        {
            var result = await _dbContext.Set<EWebRequest>().Include(x => x.WebService).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<IQueryable<EWebRequest>> SelectAllByApplicationId(string aId)
        {
            return
                await Task.FromResult(
                    _dbContext.Set<EWebRequest>()
                        .Include(x => x.WebService)
                        .Where(x => x.ApplicationId == aId)
                        .AsNoTracking()
                );
        }
    }
}