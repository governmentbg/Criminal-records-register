using MJ_CAIS.DTO.ExternalServicesHost;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEISSIntegrationService
    {
        Task SendBulletinsDataAsync(SendBulletinsDataRequestType value);

        Task SendFinesDataAsync(SendFineDataRequestType value);
    }
}
