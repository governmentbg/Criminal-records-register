using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface IRegixService
    {
        List<EWebRequest> GetRequestsForAsyncExecution();
        Task<(PersonDataResponseType, EWebRequest)> SyncCallPersonDataSearch(string egn,
            string? applicationId = null,
            string? wApplicationId = null);

        void CreateRegixRequests(string egn, string wApplicationId);

        Task<(ForeignIdentityInfoResponseType, EWebRequest)> SyncCallForeignIdentitySearchV2(string egn,
            string? applicationId = null,
            string? wApplicationId = null);

        Task<PersonDataResponseType>  ExecutePersonDataSearch(EWebRequest request, string webServiceName, string? egn = null);
        Task<RelationsResponseType> ExecuteRelationsSearch(EWebRequest request, string webServiceNameRelations, string? egn = null);
        Task<ForeignIdentityInfoResponseType> ExecuteForeignIdentitySearchV2(EWebRequest request, string webServiceName, string? egn = null);

    }
}
