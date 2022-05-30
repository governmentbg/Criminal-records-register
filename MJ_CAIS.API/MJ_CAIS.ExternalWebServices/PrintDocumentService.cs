using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL.JasperReports.Integration.Enums;
using TL.JasperReports.Integration.Interfaces;

namespace MJ_CAIS.ExternalWebServices
{
    public class PrintDocumentService : IPrintDocumentService
    {
        //private readonly ICertificateRepository _certificateRepository;
        private readonly IJasperReportsClient _jasperReportsClient;
 

        public PrintDocumentService(IJasperReportsClient jasperClient) 

        {          
            _jasperReportsClient = jasperClient;
        }

        public async Task<byte[]> PrintApplication(string applicationID)
        {
            Dictionary<string, string> inputs = new Dictionary<string, string> { { "Application_ID", applicationID } };
            byte[] fileArray = await Task.FromResult(_jasperReportsClient.RunReportBuffered(GetUrlOfCertificateReport(JasperReportsNames.Application_Report), OutputFormats.pdf, inputs).Result);
            return fileArray;

        }

        public async Task<byte[]> PrintCertificate(string certificateID, string checkUrl, JasperReportsNames reportName)
        {
            Dictionary<string, string> inputs = new Dictionary<string, string> { { "certificate_id", certificateID }, { "check_url", checkUrl } };
            byte[] fileArray = await Task.FromResult(_jasperReportsClient.RunReportBuffered(GetUrlOfCertificateReport(reportName), OutputFormats.pdf, inputs).Result);
            return fileArray;
        }

        public async Task<byte[]> PrintReport(string reportId,  JasperReportsNames reportName)
        {
            Dictionary<string, string> inputs = new Dictionary<string, string> { { "report_id", reportId }};
            byte[] fileArray = await Task.FromResult(_jasperReportsClient.RunReportBuffered(GetUrlOfCertificateReport(reportName), OutputFormats.pdf, inputs).Result);
            return fileArray;
        }

        private string GetUrlOfCertificateReport(JasperReportsNames reportName)
        {

            return $"{CertificateConstants.UrlsInJasper.REPORTS_URL}/{reportName}";
        }

    }
}
