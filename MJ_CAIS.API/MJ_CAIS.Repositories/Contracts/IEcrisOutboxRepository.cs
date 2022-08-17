using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisOutbox;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IEcrisOutboxRepository : IBaseAsyncRepository<EEcrisOutbox, string, CaisDbContext>
    {
        IQueryable<EcrisOutboxGridDTO> SelectAllWithStatusData();

        Task<EcrisOutboxDTO> SelectWithStatusDataAsync(string id);
    }
}
