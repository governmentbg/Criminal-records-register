using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class DDocContentRepository : BaseAsyncRepository<DDocContent, CaisDbContext>, IDDocContentRepository
    {
        public DDocContentRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

    }
}
