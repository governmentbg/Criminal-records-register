using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisTcnRepository : BaseAsyncRepository<EEcrisTcn, CaisDbContext>, IEcrisTcnRepository
    {
        public EcrisTcnRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetCountAsync()
        {
            var result = await _dbContext.EEcrisTcns.Where(x => x.Status == nameof(ECRISConstants.EcrisTcnStatus.New)).CountAsync();
            return result;
        }
    }
}
