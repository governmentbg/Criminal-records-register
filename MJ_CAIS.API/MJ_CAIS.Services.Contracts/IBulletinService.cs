using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinService : IBaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string>
    {
        Task<IgPageResult<BulletinGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string statusId);

        Task ChangeStatusAsync(string aInDto, string statusId);

        Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId);

        Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId);

        Task<IQueryable<DecisionDTO>> GetDecisionsByBulletinIdAsync(string aId);

        Task<IQueryable<DocumentDTO>> GetDocumentsByBulletinIdAsync(string aId);

        Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto);

        Task DeleteDocumentAsync(string documentId);

        Task<DocumentDTO> GetDocumentContentAsync(string documentId);

        Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId);
    }
}
