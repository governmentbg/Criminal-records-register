using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationWebRepository : BaseAsyncRepository<WApplication, CaisDbContext>, IApplicationWebRepository
    {
        public ApplicationWebRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
