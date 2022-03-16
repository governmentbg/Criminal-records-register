using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisMessageRepository : BaseAsyncRepository<EEcrisMessage, CaisDbContext>, IEcrisMessageRepository
    {
        public EcrisMessageRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
