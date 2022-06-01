using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MJ_CAIS.DTO.ExternalServicesHost;

namespace MJ_CAIS.ExternalServicesHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IEISSIntegrationService
    {
        [OperationContract]
        void SendBulletinsData(SendBulletinsDataRequestType value);

        [OperationContract]
        void SendFinesData(SendFineDataRequestType value);

    }

}
