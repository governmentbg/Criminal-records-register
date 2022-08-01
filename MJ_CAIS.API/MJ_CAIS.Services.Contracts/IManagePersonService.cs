using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.Services.Contracts
{
    public interface IManagePersonService
    {
        Task<PPerson> CreatePersonAsync(PersonDTO aInDto, bool autoMergePeople = false);

        PPersonH CreatePersonHistory(PPerson person);

        Task ConnectPeopleAsync(string aId, string personToBeConnected);

        Task<PPersonId> RemovePidAsync(RemovePidDTO aInDto);

        Task<PersonDTO> SelectWithBirthInfoAsync(string aId);
    }
}
