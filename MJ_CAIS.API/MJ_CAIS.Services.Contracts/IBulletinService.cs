using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinService : IBaseAsyncService<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string>
    {
        Task<IgPageResult<BulletinGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string? statusId);

        Task ChangeStatusAsync(string aInDto, string statusId);

        Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId);

        Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId);

        Task<IQueryable<DecisionDTO>> GetDecisionsByBulletinIdAsync(string aId);

        Task<IQueryable<DocumentDTO>> GetDocumentsByBulletinIdAsync(string aId);

        Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto);

        Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId);

        Task<string> InsertAsync(BulletinAddDTO aInDto);

        Task UpdateAsync(BulletinEditDTO aInDto);

        IQueryable<BulletinStatusHistoryDTO> GetStatusHistoryByBulletinId(string aId);

        Task<BulletinBaseDTO> SelectWithPersonDataAsync(string personId);

        Task<List<BulletinGridDTO>> SelectAllNoWrapAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string? statusId);
    }
}
