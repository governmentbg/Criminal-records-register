using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class InternalRequestRepository : BaseAsyncRepository<NInternalRequest, CaisDbContext>, IInternalRequestRepository
    {
        private readonly IUserContext _userContext;

        public InternalRequestRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            this._userContext = userContext;
        }

        public IQueryable<NInternalRequest> SelectAllByIdsAsync(List<string> ids)
            => this._dbContext.NInternalRequests.AsNoTracking()
                       .Where(x => ids.Contains(x.Id));
        public override IQueryable<NInternalRequest> SelectAll()
        {
            var query = this._dbContext.NInternalRequests.AsNoTracking()
                .Include(x => x.ReqStatusCodeNavigation)
                .Include(x => x.FromAuthority)
                .Include(x => x.ToAuthority);

            return query;
        }

        public override async Task<NInternalRequest> SelectAsync(string id)
        {
            return await this._dbContext.NInternalRequests.AsNoTracking()
                        .Include(x => x.FromAuthority)
                        .Include(x => x.ToAuthority)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RequestCountDTO> GetInternalRequestsCountAsync()
        {
            var inboxCount = await _dbContext.NInternalRequests.AsNoTracking()
                .CountAsync(x => x.ReqStatusCode == InternalRequestStatusTypeConstants.Sent && x.ToAuthorityId == _userContext.CsAuthorityId);

            var outboxCount = await _dbContext.NInternalRequests.AsNoTracking()
                .CountAsync(x => (x.ReqStatusCode == InternalRequestStatusTypeConstants.Cancelled ||
                    x.ReqStatusCode == InternalRequestStatusTypeConstants.Ready) &&
                 x.FromAuthorityId == _userContext.CsAuthorityId);


            return new RequestCountDTO
            {
                InboxCount = inboxCount,
                OutboxCount = outboxCount
            };
        }
    }
}
