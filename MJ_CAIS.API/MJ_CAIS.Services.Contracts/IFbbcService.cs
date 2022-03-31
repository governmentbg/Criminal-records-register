using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IFbbcService : IBaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string>
    {
        Task<IgPageResult<FbbcGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<FbbcGridDTO> aQueryOptions, string statusId);
        Task<IQueryable<FbbcDocumentDTO>> GetDocumentsByFbbcIdAsync(string aId);
        Task InsertFbbcDocumentAsync(string fbbcId, FbbcDocumentDTO aInDto);
        Task DeleteDocumentAsync(string documentId);
        Task<FbbcDocumentDTO> GetDocumentContentAsync(string documentId);
        Task ChangeStatusAsync(string aInDto, string statusId);
    }
}
