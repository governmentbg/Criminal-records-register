using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface ISearchByIdentifierService
    {
        Task<string> SearchByIdentifier(string id);

        Task<string> SearchByIdentifierLNCH(string id);
        Task CallPersonDataSearch(string egn, string registrationNumber, string applicationId, string reportApplicationId = null);
        Task CallForeignIdentitySearch(string id, string registrationNumber, string applicationId, string reportApplicationId = null);
    }
}
