using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using TL.JasperReports.Integration;
using TL.JasperReports.Integration.Enums;
using MJ_CAIS.Common.Constants;
using System.Text;
using MJ_CAIS.Common.Enums;

namespace MJ_CAIS.Services
{
    public class CertificateService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
      //  private readonly IJasperReportsClient _jasperReportsClient;

        public CertificateService(IMapper mapper, ICertificateRepository certificateRepository)//, IJasperReportsClient jasperClient)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            //_jasperReportsClient = jasperClient;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        private async Task<byte[]> CreatePdf(string certificateID, string checkUrl, JasperReportsNames reportName)
        {
            Dictionary<string, string> inputs = new Dictionary<string, string> { { "certificate_id", certificateID }, { "check_url", checkUrl } };
            //byte[] fileArray = await Task.FromResult( _jasperReportsClient.RunReportBuffered(GetUrlOfCertificateReport(reportName), OutputFormats.pdf, inputs).Result);
            //  return fileArray;
            return null;
        }

        private string GetUrlOfCertificateReport(JasperReportsNames reportName)
        {
           
            return $"reports/{reportName}";
        }

        public async Task GetCertificate(ACertificate certificate) {

            byte[] contentCertificate;
            string checkUrl = GetURLForQRCode(certificate.AccessCode1);
            if (certificate.AAppBulletins.Where(aa => aa.Approved == true).Count() == 0)
            {
               contentCertificate = await CreatePdf(certificate.Id,checkUrl, JasperReportsNames.Certificate_without_conviction);
            }
            else
            {
                contentCertificate = await CreatePdf(certificate.Id, checkUrl, JasperReportsNames.Certificate_with_conviction);
            }

            DDocument doc = new DDocument();
            doc.Id = BaseEntity.GenerateNewId();
            doc.Name = "Свидетелство за съдимост";

            DDocContent content = new DDocContent();
            content.Id = BaseEntity.GenerateNewId();
            content.MimeType = "application/pdf";
            content.Content = contentCertificate;
            content.Bytes = content.Content.Length;

            doc.DocContentId = content.Id;
            doc.DocContent = content;
            //todo: връзка със сертификата

            certificate.StatusCode = ApplicationConstants.ApplicationStatuses.CertificatePrint;
            dbContext.DDocContents.Add(content);
            dbContext.DDocuments.Add(doc);
            dbContext.ACertificates.Update(certificate);
     


        }

        private string GetURLForQRCode(string? accessCode1)
        {
            //get from db 
            string url = "";
            return url + CertificateConstants.UrlsInPublicSites.VIEW_CERTIFICATE_URL + "/" + accessCode1;
        }
    }
}
