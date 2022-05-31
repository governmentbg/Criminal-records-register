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

        public CriminalRecordsReportType GetCriminalRecordsReport(CriminalRecordsExtendedRequestType request)
        {
            return _criminalRecordsReportSrvc.GetCriminalRecordsReport(request);
        }
        public CriminalRecordsPDFResult GetCriminalRecordsReportPDF(CriminalRecordsExtendedRequestType request)
        {
            return _criminalRecordsReportSrvc.GetCriminalRecordsReportPDF(request);
        }

        public PersonIdentifierSearchResponseType PersonIdentifierSearch(PersonIdentifierSearchExtendedRequestType request)
        {
            return _criminalRecordsReportSrvc.PersonIdentifierSearch(request);
        }
    }
}
