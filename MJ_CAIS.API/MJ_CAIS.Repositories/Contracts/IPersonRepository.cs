using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IPersonRepository : IBaseAsyncRepository<PPerson, string, CaisDbContext>
    {
        Task<List<PersonGridDTO>> SelectInPageAsync(PersonSearchParamsDTO searchObj, int pageSize, int pageNumber);

        Task<List<PPersonId>> GetPersonIdsAsync(List<PersonIdTypeDTO> personIds, string personId);

        Task<PPerson> SelectWithBirthInfoAsync(string id);

        IQueryable<PersonBulletinGridDTO> GetBulletinsByPersonId(string personId);

        IQueryable<PersonApplicationGridDTO> GetApplicationsByPersonId(string personId);

        IQueryable<PersonEApplicationGridDTO> GetEApplicationsByPersonId(string personId);

        IQueryable<PersonFbbcGridDTO> GetFbbcByPersonId(string personId);

        IQueryable<PersonPidGridDTO> GetPidsByPersonId(string personId);

        IQueryable<ObjectStatusCountDTO> GetBulletinsCountByPersonId(string personId);
    }
}
