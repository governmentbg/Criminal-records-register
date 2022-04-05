using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinRepository
    {
        public BulletinRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<BBulletin> SelectAllAsync()
        {
            return this._dbContext.BBulletins.AsNoTracking()
                .Include(x => x.BulletinAuthority);
        }

        public override async Task<BBulletin> SelectAsync(string aId)
        {
            var bulletin = await _dbContext.BBulletins
               .Include(x => x.BPersNationalities)
               .Include(x => x.CsAuthority)
               .Include(x => x.BirthCountry)
               .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return bulletin;
        }
    }
}
