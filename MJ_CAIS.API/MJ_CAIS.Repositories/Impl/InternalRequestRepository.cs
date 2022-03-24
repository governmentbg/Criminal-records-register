using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class InternalRequestRepository : BaseAsyncRepository<BInternalRequest, CaisDbContext>, IInternalRequestRepository
    {
        public InternalRequestRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<BInternalRequest> SelectAllAsync()
        {
            return this._dbContext.BInternalRequests.AsNoTracking()
                .Include(x => x.Bulletin)
                .Include(x => x.ReqStatusCodeNavigation);
        }

        public override async Task<BInternalRequest> SelectAsync(string id)
        {
            return await this._dbContext.BInternalRequests.AsNoTracking()
                     .Include(x => x.ReqStatusCodeNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
