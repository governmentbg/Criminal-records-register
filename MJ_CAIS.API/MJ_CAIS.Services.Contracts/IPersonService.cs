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

        Task<PPerson> CreatePersonAsync(PersonDTO aInDto, bool autoMergePeople = false);

        Task<PersonDTO> SelectWithBirthInfoAsync(string aId);

        Task<IgPageResult<PersonBulletinGridDTO>> SelectPersonBulletinAllWithPaginationAsync(ODataQueryOptions<PersonBulletinGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonApplicationGridDTO>> SelectPersonApplicationAllWithPaginationAsync(ODataQueryOptions<PersonApplicationGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonEApplicationGridDTO>> SelectPersonEApplicationAllWithPaginationAsync(ODataQueryOptions<PersonEApplicationGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonFbbcGridDTO>> SelectPersonFbbcAllWithPaginationAsync(ODataQueryOptions<PersonFbbcGridDTO> aQueryOptions, string personId);

        Task<IgPageResult<PersonPidGridDTO>> SelectPersonPidAllWithPaginationAsync(ODataQueryOptions<PersonPidGridDTO> aQueryOptions, string personId);

        Task ConnectPeopleAsync(string aId, string personToBeConnected);

        IQueryable<ObjectStatusCountDTO> GetBulletinsCountByPersonId(string personId);
        Task<PPersonId> RemovePidAsync(RemovePidDTO aInDto);
    }
}
