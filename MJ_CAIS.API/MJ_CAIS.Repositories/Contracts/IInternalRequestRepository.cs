using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IInternalRequestRepository : IBaseAsyncRepository<NInternalRequest, string, CaisDbContext>
    {
        IQueryable<NInternalRequest> SelectAllByIdsAsync(List<string> ids);

        Task<NInternalRequest> SelectForDeleteAsync(string id);

        Task<RequestCountDTO> GetInternalRequestsCountAsync();

        IQueryable<SelectPidGridDTO> SelectAllPidsForSelection();

        IQueryable<SelectedPersonBulletinGridDTO> GetPersonBulletins(string personId);

        IQueryable<SelectedPersonBulletinGridDTO> GetSelectedBulletins(string aId);

        IQueryable<NInternalRequest> SelectAllForJudge();

        Task<SelectedPersonBulletinGridDTOExtended> GetBulletinWithPidDataByBulletinIdAsync(string aId);

        Task<SelectedPersonBulletinGridDTOExtended> GetBulletinWithPidDataByPersonIdAsync(string personId);
    }
}
