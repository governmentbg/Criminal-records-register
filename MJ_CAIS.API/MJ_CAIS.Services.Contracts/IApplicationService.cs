using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationService : IBaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string>
    {
        public Task GenerateCertificateFromApplication(AApplication application, int certificateValidityMonths = 6, string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck);

        Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
        Task<IQueryable<ApplicationDocumentDTO>> GetDocumentsByApplicationIdAsync(string aId);
        Task InsertApplicationDocumentAsync(string applicationId, ApplicationDocumentDTO aInDto);
        Task DeleteDocumentAsync(string documentId);
        Task<ApplicationDocumentDTO> GetDocumentContentAsync(string documentId);
    }
}
