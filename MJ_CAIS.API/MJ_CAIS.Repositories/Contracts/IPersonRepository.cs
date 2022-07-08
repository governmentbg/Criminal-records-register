using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IPersonRepository : IBaseAsyncRepository<PPerson, string, CaisDbContext>
    {
        Task<List<PersonGridDTO>> SelectInPageAsync(PersonGridDTO searchObj, int pageSize, int pageNumber);

        Task<List<PPersonId>> GetPersonIdsAsync(List<PersonIdTypeDTO> personIds, string personId);

        Task<PPerson> SelectWithBirthInfoAsync(string id);

        IQueryable<PersonBulletinGridDTO> GetBulletinsByPersonId(string personId);

        IQueryable<PersonApplicationGridDTO> GetApplicationsByPersonId(string personId);

        Task<IQueryable<PersonFbbcGridDTO>> GetFbbcByPersonIdAsync(string personId);
    }
}
