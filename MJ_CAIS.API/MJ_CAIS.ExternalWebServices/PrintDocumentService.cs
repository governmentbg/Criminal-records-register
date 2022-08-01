using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.ExternalWebServices.Contracts;
using TL.JasperReports.Integration.Enums;
using TL.JasperReports.Integration.Interfaces;

namespace MJ_CAIS.ExternalWebServices
{
    public class PrintDocumentService : IPrintDocumentService
    {
        private readonly IJasperReportsClient _jasperReportsClient;
 
        public PrintDocumentService(IJasperReportsClient jasperClient)
        {          
            _jasperReportsClient = jasperClient;
        }

        public async Task<byte[]> PrintApplication(string applicationID)
        {
            var inputs = new Dictionary<string, string> { { "Application_ID", applicationID } };
            var pathToReport = GetUrlOfCertificateReport(JasperReportsNames.Application_Report);
            var fileArray = await _jasperReportsClient.RunReportBuffered(pathToReport, OutputFormats.pdf, inputs);
            return fileArray;
        }

        public async Task<byte[]> PrintBulletin(string bulletinID)
        {
            var inputs = new Dictionary<string, string> { { "bulletin_id", bulletinID } };
            var pathToReport = GetUrlOfCertificateReport(JasperReportsNames.Bulletin);
            var fileArray = await _jasperReportsClient.RunReportBuffered(pathToReport, OutputFormats.pdf, inputs);
            return fileArray;
        }

        public async Task<byte[]> PrintCertificate(string certificateID, string checkUrl)
        {
            var inputs = new Dictionary<string, string> { { "certificate_id", certificateID }, { "check_url", checkUrl } };
            var pathToReport = GetUrlOfCertificateReport(JasperReportsNames.Certificate);
            var fileArray = await _jasperReportsClient.RunReportBuffered(pathToReport, OutputFormats.pdf, inputs);
            return fileArray;
        }

        public async Task<byte[]> PrintElectronicCertificate(string certificateID, string checkUrl)
        {
            var inputs = new Dictionary<string, string> { { "certificate_id", certificateID }, { "check_url", checkUrl } };
            var pathToReport = GetUrlOfCertificateReport(JasperReportsNames.Electronic_certificate);
            var fileArray = await _jasperReportsClient.RunReportBuffered(pathToReport, OutputFormats.pdf, inputs);
            return fileArray;
        }
        public async Task<byte[]> PrintExternalElectronicCertificate(string certificateID, string checkUrl)
        {
            var inputs = new Dictionary<string, string> { { "certificate_id", certificateID }, { "check_url", checkUrl } };
            var pathToReport = GetUrlOfCertificateReport(JasperReportsNames.Electronic_external_certificate);
            var fileArray = await _jasperReportsClient.RunReportBuffered(pathToReport, OutputFormats.pdf, inputs);
            return fileArray;
        }

        public async Task<byte[]> PrintReport(string reportId)
        {
            var inputs = new Dictionary<string, string> { { "report_id", reportId }};
            var pathToReport = GetUrlOfCertificateReport(JasperReportsNames.Conviction_Report);
            var fileArray = await _jasperReportsClient.RunReportBuffered(pathToReport, OutputFormats.pdf, inputs);
            return fileArray;
        }

       

        private string GetUrlOfCertificateReport(JasperReportsNames reportName)
        {
            return $"{CertificateConstants.UrlsInJasper.REPORTS_URL}/{reportName}";
        }
    }
}
