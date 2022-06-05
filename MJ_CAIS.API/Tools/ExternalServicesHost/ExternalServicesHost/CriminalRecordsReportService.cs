using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.DTO.ExternalServicesHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalServicesHost
{
    public class CriminalRecordsReportService : ICriminalRecordsReportService
    {
        MJ_CAIS.Services.Contracts.ICriminalRecordsReportService _criminalRecordsReportSrvc;
        public CriminalRecordsReportService()
        {
            _criminalRecordsReportSrvc = Program.Services.GetService< MJ_CAIS.Services.Contracts.ICriminalRecordsReportService>();
        }

        public async Task<CriminalRecordsReportType> GetCriminalRecordsReport(CriminalRecordsExtendedRequestType request)
        {
            return await _criminalRecordsReportSrvc.GetCriminalRecordsReportAsync(request);
        }
        public async Task<CriminalRecordsPDFResult> GetCriminalRecordsReportPDF(CriminalRecordsExtendedRequestType request)
        {
            return await _criminalRecordsReportSrvc.GetCriminalRecordsReportPDFAsync(request);
        }

        public async Task<PersonIdentifierSearchResponseType> PersonIdentifierSearch(PersonIdentifierSearchExtendedRequestType request)
        {
            return await _criminalRecordsReportSrvc.PersonIdentifierSearchAsync(request);
        }
        public async Task<CriminalRecordsForPeriodResponseType> GetCriminalRecordsReportForPeriod(CriminalRecordsForPeriodRequestType request)
        {
            return await _criminalRecordsReportSrvc.GetCriminalRecordsReportForPeriodAsync(request);
        }
        
    }
}
