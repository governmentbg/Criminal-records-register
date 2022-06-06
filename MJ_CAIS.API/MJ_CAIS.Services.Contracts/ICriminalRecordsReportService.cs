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
        Task<CriminalRecordsReportType> GetCriminalRecordsReportAsync(CriminalRecordsExtendedRequestType value);

        Task<CriminalRecordsPDFResult> GetCriminalRecordsReportPDFAsync(CriminalRecordsExtendedRequestType value);

        Task<PersonIdentifierSearchResponseType> PersonIdentifierSearchAsync(PersonIdentifierSearchExtendedRequestType value);
        Task<CriminalRecordsForPeriodResponseType> GetCriminalRecordsReportForPeriodAsync(CriminalRecordsForPeriodRequestType value);

    }
}
