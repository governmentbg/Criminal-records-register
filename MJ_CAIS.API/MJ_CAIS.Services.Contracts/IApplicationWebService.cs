using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.External;
using MJ_CAIS.DTO.Application.Public;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationWebService : IBaseAsyncService<PublicApplicationDTO, PublicApplicationDTO, PublicApplicationGridDTO, WApplication, string>
    {
        Task<string> GetWebApplicationTypeId();
        Task<string> GetExternalWebApplicationTypeId();

        Task CancelAsync(string id, string? userId, string? userName);

        IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId);
        Task<string> InsertPublicAsync(PublicApplicationDTO aInDto);

        IQueryable<ExternalApplicationGridDTO> SelectExternalApplications(string userId);

        Task<string?> FindDuplicate(string egn, string purposeId, string applicantId);
        Task<string> InsertExternalAsync(ExternalApplicationDTO aInDto);

        public void SetWApplicationStatus(WApplication wapplication, WApplicationStatus newStatus, string description, bool addToContext = true);

        Task<DTO.Application.Public.ApplicationPreviewDTO> GetPublicForPreviewAsync(string id);

        Task<DTO.Application.External.ApplicationPreviewDTO> GetExternalForPreviewAsync(string id);

        Task<decimal?> GetPriceByApplicationType(string applicationTypeID);
          }
}
