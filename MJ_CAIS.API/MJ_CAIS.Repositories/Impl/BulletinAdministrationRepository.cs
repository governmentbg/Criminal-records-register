using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinAdministrationRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinAdministrationRepository
    {
        private readonly IUserContext _userContext;

        public BulletinAdministrationRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            _userContext = userContext;
        }

        public override IQueryable<BBulletin> SelectAll()
        {
            var query = _dbContext.BBulletins.AsNoTracking()
                .Where(x => x.Locked == true && _userContext.CsAuthorityId == x.CsAuthorityId);

            return query;
        }

        public override async Task<BBulletin> SelectAsync(string id)
        {
            var query = await _dbContext.BBulletins
                .AsNoTracking()
                .Include(x => x.CsAuthority)
                .Include(x => x.Status)
                .Include(x => x.BulletinAuthority)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return query;
        }

        public async Task<BBulletin> GetBulletinByIdAsync(string id)
        {
            var bulletin = await _dbContext.BBulletins.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return bulletin;
        }
    }
}
