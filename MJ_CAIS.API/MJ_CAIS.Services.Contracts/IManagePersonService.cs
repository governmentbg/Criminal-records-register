using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.Services.Contracts
{
    public interface IManagePersonService
    {
        Task<PPerson> CreatePersonAsync(PersonDTO aInDto, bool autoMergePeople = false);

        void UpdatePidDataData(ICollection<PPersonId> pids, IPidsWithDocumentEntity entity);

        void UpdatePidDataData(ICollection<PPersonId> pids, IPidsEntity entity);

        PPersonH CreatePersonHistory(PPerson person, string tableName, string tableId, string tableDesc);

        Task ConnectPeopleAsync(string aId, string personToBeConnected, string desc);

        Task<PPersonId> RemovePidAsync(RemovePidDTO aInDto);

        Task<PersonDTO> SelectWithBirthInfoAsync(string aId);

        Task<string> GenerateSuidAsync(PersonDTO person);

        Task SavePersonAndUpdateSearchAttributesAsync(PPerson person, bool clearTracker = false);
    }
}
