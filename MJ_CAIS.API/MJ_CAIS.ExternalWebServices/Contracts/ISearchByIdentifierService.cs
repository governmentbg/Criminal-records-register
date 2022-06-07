using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public interface ISearchByIdentifierService
    {
        Task<(string, EWebRequest)> SearchByIdentifier(string id);

        Task<(string, EWebRequest)> SearchByIdentifierLNCH(string id);
    }
}
