using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IEcrisMessageRepository : IBaseAsyncRepository<EEcrisMessage, string, CaisDbContext>
    {
        IQueryable<ObjectStatusCountDTO> GetStatusCount();
        Task<IQueryable<EEcrisMsgNationality>> SelectAllNationalitiesAsync();
        Task<IQueryable<EEcrisMsgName>> SelectAllNamesAsync();
        IQueryable<EcrisMessageGridDTO> CustomGetAll();
        Task<IQueryable<GraoPersonGridDTO>> GetGraoPeopleAsync(string aId);
    }
}
