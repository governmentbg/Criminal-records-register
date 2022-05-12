using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class UserRepository : BaseAsyncRepository<GUser, CaisDbContext>, IUserRepository
    {
        public UserRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
