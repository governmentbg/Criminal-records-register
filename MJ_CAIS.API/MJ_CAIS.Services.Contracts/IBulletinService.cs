using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinService : IBaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string>
    {
        Task<IgPageResult<BulletinGridDTO>> GetAllCustomAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string statusId);

        Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId);

        Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId);

        Task<IQueryable<DecisionDTO>> GetDecisionsByBulletinIdAsync(string aId);

        Task<IQueryable<DocumentDTO>> GetDocumentsByBulletinIdAsync(string aId);

        Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto);

        Task DeleteComplaintDocumentAsync(string documentId);

        Task<DocumentDTO> GetDocumentContentAsync(string documentId);

        Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId);
    }
}
