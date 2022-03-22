using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IFbbcService : IBaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string>
    {
        Task<IQueryable<FbbcDocumentDTO>> GetDocumentsByFbbcIdAsync(string aId);
        Task InsertFbbcDocumentAsync(string fbbcId, FbbcDocumentDTO aInDto);
        Task DeleteDocumentAsync(string documentId);
        Task<FbbcDocumentDTO> GetDocumentContentAsync(string documentId);
    }
}
