using MJ_CAIS.DTO.Person;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Services.Contracts
{
    public interface IPersonService : IBaseAsyncService<PersonDTO, PersonDTO, PersonGridDTO, PPerson, string>
    {
        Task<IgPageResult<PersonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<PersonGridDTO> aQueryOptions, PersonSearchParamsDTO searchParams);

        Task<IgPageResult<SelectPidGridDTO>> SelectAllPidsForSelectionWithPaginationAsync(ODataQueryOptions<SelectPidGridDTO> aQueryOptions);

        Task<IgPageResult<PersonBulletinGridDTO>> SelectPersonBulletinAllWithPaginationAsync(ODataQueryOptions<PersonBulletinGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonApplicationGridDTO>> SelectPersonApplicationAllWithPaginationAsync(ODataQueryOptions<PersonApplicationGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonEApplicationGridDTO>> SelectPersonEApplicationAllWithPaginationAsync(ODataQueryOptions<PersonEApplicationGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonGeneratedReportGridDTO>> SelectPersonReportApplAllWithPaginationAsync(ODataQueryOptions<PersonGeneratedReportGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonFbbcGridDTO>> SelectPersonFbbcAllWithPaginationAsync(ODataQueryOptions<PersonFbbcGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonPidGridDTO>> SelectPersonPidAllWithPaginationAsync(ODataQueryOptions<PersonPidGridDTO> aQueryOptions, string personId);

        IQueryable<ObjectStatusCountDTO> GetBulletinsCountByPersonId(string personId);
    }
}
