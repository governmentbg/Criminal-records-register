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
        Task<ACertificate> GenerateCertificateFromApplication(AApplication application, AApplicationStatus applicationStatus, AApplicationStatus certificateWithBulletinStatus, AApplicationStatus certificateWithoutBulletinStatus, int certificateValidityMonths = 6);
        Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
     
        Task<IQueryable<PersonAliasDTO>> SelectApplicationPersAliasByApplicationIdAsync(string aId);
       
       
        Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId);
        Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId);
        Task SetApplicationStatus(AApplication application, AApplicationStatus newStatus, string description, bool includeInDbContext = true);
        Task<IgPageResult<ApplicationGridDTO>> SelectAllCertWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
        Task<ApplicationOutDTO> SelectWithPersonDataAsync(string personId);
        Task ChangeApplicationStatusToCanceled(string aId, ApplicationCancelDTO aInDto);
        Task ChangeApplicationStatusToCheckPayment(string aId, string description = "");

        Task<IQueryable<EWebRequestGridDTO>> SelectAllEWebRequestsByApplicationIdAsync(string aId);
    }
}
