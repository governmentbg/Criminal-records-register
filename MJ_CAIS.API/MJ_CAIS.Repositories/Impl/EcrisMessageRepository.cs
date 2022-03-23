using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisMessageRepository : BaseAsyncRepository<EEcrisMessage, CaisDbContext>, IEcrisMessageRepository
    {
        public EcrisMessageRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<EEcrisMessage> SelectAllAsync()
        {
            return this._dbContext.EEcrisMessages.AsNoTracking()
                .Include(x => x.EcrisMsgStatusNavigation);
        }
    }
}
