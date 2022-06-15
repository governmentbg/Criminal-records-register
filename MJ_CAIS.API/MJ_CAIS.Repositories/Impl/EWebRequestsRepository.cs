using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class EWebRequestsRepository : BaseAsyncRepository<EWebRequest, CaisDbContext>, IEWebRequestsRepository
    {
        public EWebRequestsRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }


        public virtual IQueryable<EWebRequest> SelectAll()
        {
            var result = 
                this._dbContext
                .Set<EWebRequest>()
                .Include(x => x.WebService)
                .AsNoTracking();

            return result;
        }
    }
}
