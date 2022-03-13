using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class FbbcRepository : BaseAsyncRepository<Fbbc, CaisDbContext>, IFbbcRepository
    {
        public FbbcRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
