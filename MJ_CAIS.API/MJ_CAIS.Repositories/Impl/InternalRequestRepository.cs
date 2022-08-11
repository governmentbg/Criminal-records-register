using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
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

        public async Task<int> GetCountOfNewRequestsAsync()
        {
            //var result = await
            //    _dbContext.BInternalRequests
            //        .Include(x => x.Bulletin)
            //        .CountAsync(x => x.Bulletin.CsAuthorityId == _userContext.CsAuthorityId && x.ReqStatusCode == InternalRequestStatusTypeConstants.New);

            //return result;
            throw new NotImplementedException();
        }

        public async Task<bool> HasRequests(NInternalRequest entity, List<string> bullIdsForCert)
        {
            //return await _dbContext.BInternalRequests.AsNoTracking()
            //    .AnyAsync(x => x.ReqStatusCode == InternalRequestStatusTypeConstants.New &&
            //    x.Id != entity.Id &&
            //    bullIdsForCert.Contains(x.AAppBulletinId));
            throw new NotImplementedException();
        }

        public async Task<AAppBulletin> GetBulletinsInCertificate(NInternalRequest entity)
        {
            //return await _dbContext.AAppBulletins.AsNoTracking()
            //    .Include(x => x.Certificate)
            //   .FirstOrDefaultAsync(x => x.Id == entity.AAppBulletinId);
            throw new NotImplementedException();
        }
    }
}
