using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinRepository
    {
        public BulletinRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
