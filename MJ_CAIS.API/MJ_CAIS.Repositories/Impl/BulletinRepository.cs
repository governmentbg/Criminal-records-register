using MJ_CAIS.DataAccess;
using MJ_CAIS.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<Bulletin, CaisDbContext>, IBulletinRepository
    {
        public BulletinRepository(CaisDbContext context) : base(context)
        {
        }
    }
}
