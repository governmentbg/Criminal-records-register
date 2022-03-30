using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class IsinDataRepository : BaseAsyncRepository<EIsinDatum, CaisDbContext>, IIsinDataRepository
    {
        public IsinDataRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<EIsinDatum> SelectAllAsync()
        {
            return _dbContext.EIsinData.AsNoTracking()
                .Include(x => x.IsinMsg);
        }
    }
}
