using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisInbox;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisInboxRepository : BaseAsyncRepository<EEcrisInbox, CaisDbContext>, IEcrisInboxRepository
    {
        public EcrisInboxRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<EcrisInboxGridDTO> SelectAllWithStatusData()
        {
            var query = from ecrisInbox in _dbContext.EEcrisInboxes.AsNoTracking()
                        join status in _dbContext.EEcrisCommunStatuses.AsNoTracking()
                         on ecrisInbox.Status equals status.Code into statusLeft
                        from status in statusLeft.DefaultIfEmpty()
                        select new EcrisInboxGridDTO
                        {
                            Id = ecrisInbox.Id,
                            CreatedOn = ecrisInbox.CreatedOn,
                            EcrisMsgId = ecrisInbox.EcrisMsgId,
                            HasError = ecrisInbox.HasError,
                            ImportedOn = ecrisInbox.ImportedOn,
                            Status = ecrisInbox.Status,
                            Version = ecrisInbox.Version,
                            StatusName = status.Name
                        };

            return query;
        }

        public async Task<EcrisInboxDTO> SelectWithStatusDataAsync(string id)
        {
            var query = await (from ecrisInbox in _dbContext.EEcrisInboxes.AsNoTracking()
                               join status in _dbContext.EEcrisCommunStatuses.AsNoTracking()
                                on ecrisInbox.Status equals status.Code into statusLeft
                               from status in statusLeft.DefaultIfEmpty()
                               select new EcrisInboxDTO
                               {
                                   Id = ecrisInbox.Id,
                                   CreatedOn = ecrisInbox.CreatedOn,
                                   EcrisMsgId = ecrisInbox.EcrisMsgId,
                                   HasError = ecrisInbox.HasError,
                                   ImportedOn = ecrisInbox.ImportedOn,
                                   Version = ecrisInbox.Version,
                                   Status = status.Name,
                                   StackTrace = ecrisInbox.StackTrace,
                                   Error = ecrisInbox.Error,
                                   XmlMessage = ecrisInbox.XmlMessage,
                                   XmlMessageTraits = ecrisInbox.XmlMessageTraits,
                               }).FirstOrDefaultAsync(x => x.Id == id);

            return query;
        }
    }
}
