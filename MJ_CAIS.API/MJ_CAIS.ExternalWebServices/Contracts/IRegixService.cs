using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface IRegixService
    {
        List<EWebRequest> GetRequestsForAsyncExecution();
        Task<(PersonDataResponseType, EWebRequest)> SyncCallPersonDataSearch(string egn,
            string applicationId, string registrationNumber);

        void CreateRegixRequests(string egn, string wApplicationId);

        Task<(ForeignIdentityInfoResponseType, EWebRequest)> SyncCallForeignIdentitySearchV2(string lnch,
            string applicationId, string registrationNumber);

        Task<PersonDataResponseType>  ExecutePersonDataSearch(EWebRequest request, string webServiceName, string? egn = null, string? registrationNumber = null);
        Task<RelationsResponseType> ExecuteRelationsSearch(EWebRequest request, string webServiceNameRelations, string? egn = null, string? registrationNumber = null);
        Task<ForeignIdentityInfoResponseType> ExecuteForeignIdentitySearchV2(EWebRequest request, string webServiceName, string? egn = null, string? registrationNumber = null);

    }
}
