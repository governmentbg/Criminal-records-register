using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Documents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface IDocumentService : IBaseAsyncService<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, DDocument, string>
    {
        Task InsertDocumentAsync(string? applicationId, string? bulletinId, string? fbbcId, DocumentDTO aInDto);
        Task DeleteDocumentAsync(string documentId);
        Task<IQueryable<DocumentDTO>> GetDocumentsByApplicationIdAsync(string aId);
        Task<DocumentDTO> GetDocumentContentAsync(string documentId);
        Task<IQueryable<DocumentDTO>> GetDocumentsByFbbcIdAsync(string aId);


    }
}
