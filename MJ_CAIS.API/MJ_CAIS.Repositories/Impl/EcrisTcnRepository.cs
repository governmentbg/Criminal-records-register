using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisTcnRepository : BaseAsyncRepository<EEcrisTcn, CaisDbContext>, IEcrisTcnRepository
    {
        public EcrisTcnRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
