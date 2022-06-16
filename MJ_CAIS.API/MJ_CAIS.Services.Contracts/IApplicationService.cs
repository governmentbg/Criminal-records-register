using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.EWebRequest;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationService : IBaseAsyncService<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, AApplication, string>
    {
        Task UpdateAsync(ApplicationInDTO aInDto, bool isFinal);
        Task<string> GenerateCertificateFromApplication(string id);
        Task GenerateCertificateFromApplication(AApplication application, AApplicationStatus applicationStatus, AApplicationStatus certificateWithBulletinStatus, AApplicationStatus certificateWithoutBulletinStatus, int certificateValidityMonths = 6);
        Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
        Task<IQueryable<ApplicationDocumentDTO>> GetDocumentsByApplicationIdAsync(string aId);
        Task<IQueryable<PersonAliasDTO>> SelectApplicationPersAliasByApplicationIdAsync(string aId);
        Task InsertApplicationDocumentAsync(string applicationId, ApplicationDocumentDTO aInDto);
        Task DeleteDocumentAsync(string documentId);
        Task<ApplicationDocumentDTO> GetDocumentContentAsync(string documentId);
        Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId);
        Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId);
        public void SetApplicationStatus(AApplication application, AApplicationStatus newStatus, string description, bool includeInDbContext = true);
        Task<IgPageResult<ApplicationGridDTO>> SelectAllCertWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
        Task<ApplicationOutDTO> SelectWithPersonDataAsync(string personId);
        Task ChangeApplicationStatusToCanceled(string aId);
        Task ChangeApplicationStatusToCheckPayment(string aId);

        Task<IQueryable<EWebRequestGridDTO>> SelectAllEWebRequestsByApplicationIdAsync(string aId);
    }
}
