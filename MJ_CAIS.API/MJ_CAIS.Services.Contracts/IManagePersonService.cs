using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.Services.Contracts
{
    public interface IManagePersonService
    {
        Task<PPerson> CreatePersonAsync(PersonDTO aInDto, bool autoMergePeople = false);

        Task ConnectPeopleAsync(string aId, string personToBeConnected);

        Task<PPersonId> RemovePidAsync(RemovePidDTO aInDto);

        Task<PersonDTO> SelectWithBirthInfoAsync(string aId);

        Task<List<PersonIdTypeDTO>> GetPidsFromFormAsync(PersonDTO aInDto);

        void GeneratePersonCitizenship(PPerson person, PPersonH personH, IEnumerable<string> nationalities);

        PPerson CreateNewPerson(PersonDTO aInDto, List<PPersonId> pids, string personId);

        PPersonH CreateNewPersonHistory(PPerson person);
    }
}
