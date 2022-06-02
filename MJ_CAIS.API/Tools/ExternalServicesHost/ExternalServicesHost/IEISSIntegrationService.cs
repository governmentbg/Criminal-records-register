using MJ_CAIS.DTO.ExternalServicesHost;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalServicesHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IEISSIntegrationService
    {
        [OperationContract]
        Task SendBulletinsData(SendBulletinsDataRequestType value);

        [OperationContract]
        Task SendFinesData(SendFineDataRequestType value);
    }
}
