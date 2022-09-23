using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    internal class ViewsCountsRepository : BaseAsyncRepository<VBulletin, CaisDbContext>, IViewsCountsRepository
    {

        private readonly IUserContext _userContext;

        public ViewsCountsRepository(CaisDbContext dbContext, IUserContext userContext) : base(dbContext)
        {
            _userContext = userContext;
        }

        public IQueryable<ObjectStatusCountDTO> GetCentralAuthorityCounts()
        {
            var query = _dbContext.VCntCentralAuths.AsNoTracking()
                 .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId)
                 .Select(x => new ObjectStatusCountDTO
                 {
                     Status = x.Type,
                     Count = (int)x.Cnt
                 });

            return query;
        }

        public IQueryable<ObjectStatusCountDTO> GetApplicationsCountByCurrentAuthority()
        {
            var query = _dbContext.VCntApplications.AsNoTracking()
                 .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId)
                 .Select(x => new ObjectStatusCountDTO
                 {
                     Status = x.Type,
                     Count = (int)x.Cnt
                 });

            return query;
        }

        public IQueryable<ObjectStatusCountDTO> GetBulletinsStatusCountByCurrentAuthority()
        {
            var query = _dbContext.VCntBulletins.AsNoTracking()
                 .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId)
                 .Select(x => new ObjectStatusCountDTO
                 {
                     Status = x.Type,
                     Count = (int)x.Cnt
                 });

            return query;
        }
    }
}
