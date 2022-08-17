using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisOutbox;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisOutboxRepository : BaseAsyncRepository<EEcrisOutbox, CaisDbContext>, IEcrisOutboxRepository
    {
        public EcrisOutboxRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<EcrisOutboxGridDTO> SelectAllWithStatusData()
        {
            var query = from ecrisOutbox in _dbContext.EEcrisOutboxes.AsNoTracking()
                        join status in _dbContext.EEcrisCommunStatuses.AsNoTracking()
                         on ecrisOutbox.Status equals status.Code into statusLeft
                        from status in statusLeft.DefaultIfEmpty()
                        select new EcrisOutboxGridDTO
                        {
                            Id = ecrisOutbox.Id,
                            CreatedOn = ecrisOutbox.CreatedOn,
                            EcrisMsgId = ecrisOutbox.EcrisMsgId,
                            HasError = ecrisOutbox.HasError,
                            Status = ecrisOutbox.Status,
                            Version = ecrisOutbox.Version,
                            StatusName = status.Name,
                            Attempts = ecrisOutbox.Attempts,
                            ExecutionDate = ecrisOutbox.ExecutionDate,
                            Operation = ecrisOutbox.Operation,
                            Error = ecrisOutbox.Error,
                        };

            return query;
        }

        public async Task<EcrisOutboxDTO> SelectWithStatusDataAsync(string id)
        {
            var query = await (from ecrisOutbox in _dbContext.EEcrisOutboxes.AsNoTracking()
                               join status in _dbContext.EEcrisCommunStatuses.AsNoTracking()
                                on ecrisOutbox.Status equals status.Code into statusLeft
                               from status in statusLeft.DefaultIfEmpty()
                               select new EcrisOutboxDTO
                               {
                                   Id = ecrisOutbox.Id,
                                   EcrisMsgId = ecrisOutbox.EcrisMsgId,
                                   HasError = ecrisOutbox.HasError,
                                   Version = ecrisOutbox.Version,
                                   Status = status.Name,
                                   StackTrace = ecrisOutbox.StackTrace,
                                   Error = ecrisOutbox.Error,
                                   Operation = ecrisOutbox.Operation,
                                   ExecutionDate = ecrisOutbox.ExecutionDate,
                                   Attempts = ecrisOutbox.Attempts,
                                   XmlObject = ecrisOutbox.XmlObject,
                               }).FirstOrDefaultAsync(x => x.Id == id);

            return query;
        }
    }
}
