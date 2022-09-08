using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
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

        Task<IQueryable<PersonArchiveGridDTO>> GetArchiveByPersonIdAsync(string personId);

        IQueryable<PersonEApplicationGridDTO> GetEApplicationsByPersonId(string personId);

        IQueryable<PersonGeneratedReportGridDTO> GetAllReportApplByPersonId(string personId);

        IQueryable<PersonFbbcGridDTO> GetFbbcByPersonId(string personId);

        IQueryable<PersonPidGridDTO> GetPidsByPersonId(string personId);

        IQueryable<ObjectStatusCountDTO> GetBulletinsCountByPersonId(string personId);

        Task<PPersonId> GetPersonIdByIdAsync(string pidId);

        Task<PPerson> GetExistingPersonWithPidsDataAsync(string id);

        IQueryable<PPerson> GetExistingPeopleWithPidsData(IEnumerable<string> ids);

        IQueryable<PPerson> GetPeopleToBeConectedWithPidData(string firstPersonId, string secondPersonId);

        Task<string> GetIsoNumberByCountryIdAsync(string countryId);

        Task<List<PPerson>> GetPersonByID(IQueryable<string> personIds);

        Task<IQueryable<string>> GetPersonIDsByPersonData(string? firstname, string? surname, string? familyname, string? birthCountry, DateTime birthdate, string birthDatePrec, string? birthplace, string? fullname, DateTime birthdateFrom, DateTime birthdateTo, int birthdateYear);

        Task<List<CriminalRecordsPersonDataType>> GetPersonsByPersonData(string? firstname, string? surname, string? familyname, string? birthCountry, DateTime birthdate, string birthDatePrec, string? birthplace, string? fullname);

        IQueryable<PersonHistoryDataGridDTO> GetPersonHistoryDataByPersonId(string personId);
    }
}
