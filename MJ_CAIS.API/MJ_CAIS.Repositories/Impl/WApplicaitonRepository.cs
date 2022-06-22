using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class WApplicaitonRepository : BaseAsyncRepository<WApplication, CaisDbContext>, IWApplicaitonRepository
    {
        public WApplicaitonRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<WApplication> SelectAll()
        {
            return _dbContext.WApplications
                .Include(x => x.PurposeNavigation)
                .Include(x => x.PaymentMethod);
        }
    }
}
