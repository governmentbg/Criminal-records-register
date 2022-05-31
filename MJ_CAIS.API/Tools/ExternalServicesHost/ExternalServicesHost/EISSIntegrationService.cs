using MJ_CAIS.DTO.ExternalServicesHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MJ_CAIS.ExternalServicesHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EISSIntegrationService : IEISSIntegrationService
    {
        public void SendBulletinsData(SendBulletinsDataRequestType value)
        {
            throw new NotImplementedException();
        }

        
        public void SendFinesData(SendFineDataRequestType value)
        {
            throw new NotImplementedException();
        }
    }
}
