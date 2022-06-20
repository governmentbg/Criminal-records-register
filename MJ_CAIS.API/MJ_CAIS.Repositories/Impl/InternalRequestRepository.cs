using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class InternalRequestRepository : BaseAsyncRepository<BInternalRequest, CaisDbContext>, IInternalRequestRepository
    {
        private readonly IUserContext _userContext;

        public InternalRequestRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            this._userContext = userContext;
        }

        public override IQueryable<BInternalRequest> SelectAll()
        {
            var query = this._dbContext.BInternalRequests.AsNoTracking()
                .Include(x => x.Bulletin)
                .Include(x => x.ReqStatusCodeNavigation)
                .Where(x => x.Bulletin.CsAuthorityId == _userContext.CsAuthorityId);

            return query;
        }

        public override async Task<BInternalRequest> SelectAsync(string id)
        {
            return await this._dbContext.BInternalRequests.AsNoTracking()
                     .Include(x => x.Bulletin)
                     .Include(x => x.ReqStatusCodeNavigation)
                     .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCountOfNewRequestsAsync()
        {
            var result = await
                _dbContext.BInternalRequests
                    .Include(x => x.Bulletin)
                    .CountAsync(x => x.Bulletin.CsAuthorityId == _userContext.CsAuthorityId && x.ReqStatusCode == InternalRequestStatusTypeConstants.New);

            return result;
        }
    }
}
