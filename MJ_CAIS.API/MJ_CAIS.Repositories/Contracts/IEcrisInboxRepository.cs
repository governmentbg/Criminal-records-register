using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisInbox;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IEcrisInboxRepository : IBaseAsyncRepository<EEcrisInbox, string, CaisDbContext>
    {
        IQueryable<EcrisInboxGridDTO> SelectAllWithStatusData();

        Task<EcrisInboxDTO> SelectWithStatusDataAsync(string id);
    }
}
