using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;
using static MJ_CAIS.Common.Constants.ECRISConstants;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisMessageRepository : BaseAsyncRepository<EEcrisMessage, CaisDbContext>, IEcrisMessageRepository
    {
        public EcrisMessageRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.EEcrisMessages.AsNoTracking()
                 .Where(x => x.EcrisMsgStatus == EcrisMessageStatuses.ForIdentification ||
                             x.EcrisMsgStatus == EcrisMessageStatuses.ReqWaitingForCSAuthority)
                .GroupBy(x => x.EcrisMsgStatus)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }
    }
}
