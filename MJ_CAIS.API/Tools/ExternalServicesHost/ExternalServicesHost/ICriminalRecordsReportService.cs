using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using MJ_CAIS.IntegrationModel;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalServicesHost
{
    [ServiceContract]
    public interface  ICriminalRecordsReportService
    {
        [OperationContract]
        CriminalRecordsReportType GetCriminalRecordsReport(CriminalRecordsExtendedRequestType value);

        [OperationContract]
        PersonIdentifierSearchResponseType PersonIdentifierSearch(PersonIdentifierSearchExtendedRequestType value);
    }
}
