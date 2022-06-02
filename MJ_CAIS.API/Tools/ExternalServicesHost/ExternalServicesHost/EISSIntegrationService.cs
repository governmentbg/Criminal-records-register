using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.DTO.ExternalServicesHost;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalServicesHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EISSIntegrationService : IEISSIntegrationService
    {
        private readonly Services.Contracts.IEISSIntegrationService _eISSIntegrationService;

        public EISSIntegrationService()
        {
            _eISSIntegrationService = Program.Services.GetService<MJ_CAIS.Services.Contracts.IEISSIntegrationService>(); 
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
