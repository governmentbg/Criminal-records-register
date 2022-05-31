using MJ_CAIS.DTO.ExternalServicesHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface ICriminalRecordsReportService
    {
        CriminalRecordsReportType GetCriminalRecordsReport(CriminalRecordsExtendedRequestType value);

        CriminalRecordsPDFResult GetCriminalRecordsReportPDF(CriminalRecordsExtendedRequestType value);

        PersonIdentifierSearchResponseType PersonIdentifierSearch(PersonIdentifierSearchExtendedRequestType value);
    }
}
