using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class RoleRepository : BaseAsyncRepository<GRole, CaisDbContext>, IRoleRepository
    {
        public RoleRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
