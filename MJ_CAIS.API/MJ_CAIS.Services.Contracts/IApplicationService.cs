using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationService : IBaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string>
    {
        public Task GenerateCertificateFromApplication(AApplication application,string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsForPurpose);
        IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId);

        Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
    }
}
