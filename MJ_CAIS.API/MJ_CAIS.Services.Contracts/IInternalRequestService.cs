using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IInternalRequestService : IBaseAsyncService<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, NInternalRequest, string>
    {
        Task<IgPageResult<InternalRequestGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string statuses, bool fromAuth);

        Task<IgPageResult<InternalRequestForJudgeGridDTO>> SelectAllForJudgeWithPaginationAsync(ODataQueryOptions<InternalRequestForJudgeGridDTO> aQueryOptions, string statuses);

        Task<IgPageResult<SelectPidGridDTO>> SelectAllPidsForSelectionWithPaginationAsync(ODataQueryOptions<SelectPidGridDTO> aQueryOptions);

        Task ChangeStatusAsync(string aId, string status);

        Task<RequestCountDTO> GetInternalRequestsCount();

        Task ReplayAsync(string aId, bool accepted, string responseDesc);

        Task MarkAsReaded(List<string> ids);

        IQueryable<SelectedPersonBulletinGridDTO> GetPersonBulletins(string personId);

        IQueryable<SelectedPersonBulletinGridDTO> GetSelectedBulletins(string aId);
    }
}
