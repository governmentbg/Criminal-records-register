using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using TL.JasperReports.Integration;
using TL.JasperReports.Integration.Enums;
using TL.JasperReports.Integration.Interfaces;
using TL.Signer;

namespace MJ_CAIS.ExternalWebServices
{
    public class CertificateGenerationService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateGenerationService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IJasperReportsClient _jasperReportsClient;
        private readonly IPdfSigner _pdfSignerService;

        public CertificateGenerationService(IMapper mapper, ICertificateRepository certificateRepository, IJasperReportsClient jasperClient,
            IPdfSigner pdfSignerService)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
           _jasperReportsClient = jasperClient;
            _pdfSignerService = pdfSignerService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
        public async Task<byte[]> CreateCertificate(string certificateID)
        {
            var certificate = await dbContext.ACertificates
                                    .Include(x=>x.AAppBulletins).FirstOrDefaultAsync(x => x.Id == certificateID);
            if (certificate == null)
            {
                //todo: resources and EH
                throw new Exception($"Certificate with ID {certificateID} does not exist.");
            }
            return await CreateCertificate(certificate, await GetWebPortalAddress());

        }
        public async Task<byte[]> CreateCertificate(ACertificate certificate, string? webportalUrl = null, string statusCode = ApplicationConstants.ApplicationStatuses.CertificateServerSign)
        {

            byte[] contentCertificate;
            string checkUrl = await GetURLForQRCodeAsync(certificate.AccessCode1, webportalUrl);
            if (certificate.AAppBulletins.Where(aa => aa.Approved == true).Count() == 0)
            {
                contentCertificate = await CreatePdf(certificate.Id, checkUrl, JasperReportsNames.Certificate_without_conviction);
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
                  
            certificate.DocId = doc.Id;
            certificate.StatusCode = statusCode;
            dbContext.DDocContents.Add(content);
            dbContext.DDocuments.Add(doc);
            dbContext.ACertificates.Update(certificate);

            return contentCertificate;

        }

        private async Task<byte[]> CreatePdf(string certificateID, string checkUrl, JasperReportsNames reportName)
        {
            Dictionary<string, string> inputs = new Dictionary<string, string> { { "certificate_id", certificateID }, { "check_url", checkUrl } };
            byte[] fileArray = await Task.FromResult( _jasperReportsClient.RunReportBuffered(GetUrlOfCertificateReport(reportName), OutputFormats.pdf, inputs).Result);
           
            fileArray = _pdfSignerService.SignPdf(fileArray, SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME);
          
            return fileArray;
          
        }

        private string GetUrlOfCertificateReport(JasperReportsNames reportName)
        {

            return $"{CertificateConstants.UrlsInJasper.REPORTS_URL}/{reportName}";
        }

     

        private async Task<string> GetURLForQRCodeAsync(string? accessCode1, string webportalUrl)
        {
            var url = webportalUrl;
            if (string.IsNullOrEmpty(webportalUrl))
            {
                url = await GetWebPortalAddress();
                if (url == null)
                {
                    //todo:resources & EH
                    throw new Exception("Web portal URL is not set.");
                }
            }
            return $"{url}/{CertificateConstants.UrlsInPublicSites.VIEW_CERTIFICATE_URL}/{accessCode1}";
        }
        public async Task<string?> GetWebPortalAddress()
        {
            return (await dbContext.GSystemParameters.FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.WEB_PORTAL_URL))?.ValueString;
        }



    }
}
