using MJ_CAIS.DTO.ExternalServicesHost;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalServicesHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EISSIntegrationService : IEISSIntegrationService
    {
        private readonly Services.Contracts.IEISSIntegrationService _eISSIntegrationService;

        public EISSIntegrationService(Services.Contracts.IEISSIntegrationService eISSIntegrationService)
        {
            _eISSIntegrationService = eISSIntegrationService;
        }

        public async Task SendBulletinsData(SendBulletinsDataRequestType value)
        {
            await _eISSIntegrationService.SendBulletinsDataAsync(value);
        }

        public async Task SendFinesData(SendFineDataRequestType value)
        {
            await _eISSIntegrationService.SendFinesDataAsync(value);
        }
    }
}
