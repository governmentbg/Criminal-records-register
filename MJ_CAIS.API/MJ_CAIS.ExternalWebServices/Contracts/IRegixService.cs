using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface IRegixService
    {
        List<EWebRequest> GetRequestsForAsyncExecution();
        public (PersonDataResponseType, EWebRequest) SyncCallPersonDataSearch(string egn,
            string? applicationId = null,
            string? wApplicationId = null);

        void CreateRegixRequests(string egn, string wApplicationId);

        (ForeignIdentityInfoResponseType, EWebRequest) SyncCallForeignIdentitySearchV2(string egn,
            string? applicationId = null,
            string? wApplicationId = null);

        PersonDataResponseType ExecutePersonDataSearch(EWebRequest request, string webServiceName, string? egn = null);
        RelationsResponseType ExecuteRelationsSearch(EWebRequest request, string webServiceNameRelations, string? egn = null);
        ForeignIdentityInfoResponseType ExecuteForeignIdentitySearchV2(EWebRequest request, string webServiceName, string? egn = null);

    }
}
