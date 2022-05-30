using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinAdministrationRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinAdministrationRepository
    {
        public BulletinAdministrationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<BBulletin> SelectAllAsync()
        {
            var query = _dbContext.BBulletins.AsNoTracking()
                .Where(x => x.Locked == true);

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
    }
}
