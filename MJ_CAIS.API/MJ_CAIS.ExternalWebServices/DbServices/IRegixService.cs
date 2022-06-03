using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface IRegixService
    {
        List<EWebRequest> GetRequestsForAsyncExecution();

        (PersonDataResponseType, EWebRequest) SyncCallPersonDataSearch(string egn,
            string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? ecrisMsgId = null);

        PersonDataResponseType ExecutePersonDataSearch(EWebRequest request, string webServiceName);
    }
}
