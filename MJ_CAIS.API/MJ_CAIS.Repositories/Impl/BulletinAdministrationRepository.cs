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
    }
}
