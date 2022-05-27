using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IPersonRepository : IBaseAsyncRepository<PPerson, string, CaisDbContext>
    {
        Task<List<PersonGridDTO>> SelectInPageAsync(PersonGridDTO searchObj, int pageSize, int pageNumber);

        Task<PPersonId> GetPersonIdAsync(string pid, string pidType,string personId);

        Task<PPerson> SelectWithBirthInfoAsync(string id);

        Task<IQueryable<PersonBulletinGridDTO>> GetBulletinByPersonIdAsync(string personId);

        Task<IQueryable<PersonApplicationGridDTO>> GetApplicationsByPersonIdAsync(string personId);

        Task<IQueryable<PersonFbbcGridDTO>> GetFbbcByPersonIdAsync(string personId);
    }
}
