using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class OffenceCategoryRepository : BaseAsyncRepository<BOffenceCategory, CaisDbContext>, IOffenceCategoryRepository
    {
        public OffenceCategoryRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<BOffenceCategory> SelectAllAsync()
        {
            return this._dbContext.BOffenceCategories.AsNoTracking()
                .Where(x => x.OffLevel == 1)
                .OrderBy(x => x.OrderNumber);
        }
    }
}