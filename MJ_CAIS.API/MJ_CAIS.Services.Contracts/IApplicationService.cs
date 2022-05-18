using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationService : IBaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string>
    {
        public Task GenerateCertificateFromApplication(AApplication application,string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsForPurpose);
    }
}
