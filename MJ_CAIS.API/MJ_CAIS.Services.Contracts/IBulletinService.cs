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

        Task<IgPageResult<BulletinGridDTO>> SearchBulletinAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, BulletinSearchParamDTO searchParams);

        Task<List<BulletinGridDTO>> ExportAllAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, BulletinSearchParamDTO searchParams);

        Task ChangeStatusAsync(string aInDto, string statusId);

        Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId);

        Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId);

        Task<IQueryable<DecisionDTO>> GetDecisionsByBulletinIdAsync(string aId);

        Task<IQueryable<DocumentDTO>> GetDocumentsByBulletinIdAsync(string aId);

        Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto);

        Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId);

        Task<string> InsertAsync(BulletinAddDTO aInDto);

        Task UpdateAsync(BulletinEditDTO aInDto, bool isFinalEdit);

        IQueryable<BulletinStatusHistoryDTO> GetStatusHistoryByBulletinId(string aId);

        Task<byte[]> GetHistoryContentAsync(string aId);

        Task<BulletinBaseDTO> SelectWithPersonDataAsync(string personId);

        bool AddBulletinStatusH(BBulletin itemToBeUpdated, string oldStatus, string newStatus);

        void UpdateDeleteDateData(BBulletin entity);

        void SetEcrisConvId(BBulletin bulletin);

        Task<IQueryable<BulletinConvictionDTO>> GetConvictionOnlyAsync(string aId);

        Task DeleteBulletinByIdAsync(string id, string desc);
    }
}
