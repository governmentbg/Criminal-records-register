using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface IDocumentService : IBaseAsyncService<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, DDocument, string>
    {
        Task InsertApplicationDocumentAsync(string applicationId, ApplicationDocumentDTO aInDto);
        Task DeleteDocumentAsync(string documentId);
        Task<IQueryable<ApplicationDocumentDTO>> GetDocumentsByApplicationIdAsync(string aId);
        Task<ApplicationDocumentDTO> GetDocumentContentAsync(string documentId);
    }
}
