using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class InternalRequestRepository : BaseAsyncRepository<BInternalRequest, CaisDbContext>, IInternalRequestRepository
    {
        public InternalRequestRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<BInternalRequest> SelectAll()
        {
            var query = this._dbContext.BInternalRequests.AsNoTracking()
                .Include(x => x.Bulletin)
                .Include(x => x.ReqStatusCodeNavigation);

            return query;
        }

        public override async Task<BInternalRequest> SelectAsync(string id)
        {
            return await this._dbContext.BInternalRequests.AsNoTracking()
                     .Include(x=>x.Bulletin)
                     .Include(x => x.ReqStatusCodeNavigation)
                     .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
