using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface ISearchByIdentifierService
    {
        Task<string> SearchByIdentifier(string id);

        Task<string> SearchByIdentifierLNCH(string id);
    }
}
