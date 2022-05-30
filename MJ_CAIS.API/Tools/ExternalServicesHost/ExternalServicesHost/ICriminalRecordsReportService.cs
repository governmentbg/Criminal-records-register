using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MJ_CAIS.DTO.ExternalServicesHost;

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
