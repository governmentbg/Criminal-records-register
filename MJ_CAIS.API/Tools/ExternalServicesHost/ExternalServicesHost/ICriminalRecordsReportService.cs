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
    [XmlSerializerFormat]
    public interface  ICriminalRecordsReportService
    {
        [OperationContract]
        Task<CriminalRecordsReportType> GetCriminalRecordsReport(CriminalRecordsExtendedRequestType value);

        [OperationContract]
        Task<CriminalRecordsPDFResult> GetCriminalRecordsReportPDF(CriminalRecordsExtendedRequestType value);
        
        [OperationContract]
        Task<PersonIdentifierSearchResponseType> PersonIdentifierSearch(PersonIdentifierSearchExtendedRequestType value);
    }
}
