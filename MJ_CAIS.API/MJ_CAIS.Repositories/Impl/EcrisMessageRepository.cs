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

        public IQueryable<ObjectStatusCountDTO> GetStatusCount()
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

            return query;
        }

        public async Task<IQueryable<EEcrisMsgNationality>> SelectAllNationalitiesAsync()
        {
            var query = _dbContext.EEcrisMsgNationalities
                 .AsNoTracking()
                 .Include(x => x.Country);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<EEcrisMsgName>> SelectAllNamesAsync()
        {
            var query = _dbContext.EEcrisMsgNames
                 .AsNoTracking();

            return await Task.FromResult(query);
        }
    }
}
