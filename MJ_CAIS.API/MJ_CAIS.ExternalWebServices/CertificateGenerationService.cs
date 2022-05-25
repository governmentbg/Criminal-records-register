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
using TL.Signer;

namespace MJ_CAIS.ExternalWebServices
{
    public class CertificateGenerationService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateGenerationService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IPdfSigner _pdfSignerService;
        private readonly IPrintDocumentService _printerService;

        public CertificateGenerationService(IMapper mapper, ICertificateRepository certificateRepository, 
            IPdfSigner pdfSignerService, IPrintDocumentService printerService)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
         
            _pdfSignerService = pdfSignerService;
            _printerService = printerService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
        public async Task<byte[]> CreateCertificate(string certificateID)
        {
            var certificate = await dbContext.ACertificates
                                    .Include(c => c.AAppBulletins)
                                    .Include(c => c.Application)
                                    .Include(c => c.Application).ThenInclude(c1=>c1.Purpose)
                                    .Include(c => c.Application).ThenInclude(c1=>c1.SrvcResRcptMeth)
                                    .FirstOrDefaultAsync(x => x.Id == certificateID);
            if (certificate == null)
            {
                //todo: resources and EH
                throw new Exception($"Certificate with ID {certificateID} does not exist.");
            }
            var signingCertificateName = (await dbContext.GSystemParameters
                                    .FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME))?.ValueString;
            if (string.IsNullOrEmpty(signingCertificateName))
            {//todo: EH & resources
                throw new Exception($"Системният параметър {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME} не е настроен.");
            }
            //todo:get patterns for mail if needed:
            return await CreateCertificate(certificate, null, null, signingCertificateName, await GetWebPortalAddress());

        }
        public async Task<byte[]> CreateCertificate(ACertificate certificate, string mailSubjectPattern,
            string mailBodyPattern, string signingCertificateName, string? webportalUrl = null, string statusCodeCertificateServerSign = ApplicationConstants.ApplicationStatuses.CertificateServerSign
            , string statusCodeCertificateForDelivery = ApplicationConstants.ApplicationStatuses.CertificateForDelivery
            , string statusCodeCertificatePaperPrint = ApplicationConstants.ApplicationStatuses.CertificatePaperPrint)
        {

            byte[] contentCertificate;
            string checkUrl = await GetURLForQRCodeAsync(certificate.AccessCode1, webportalUrl);
            bool containsBulletins = false;
            if (certificate.AAppBulletins.Where(aa => aa.Approved == true).Count() == 0)
            {
                contentCertificate = await CreatePdf(certificate.Id, checkUrl, JasperReportsNames.Certificate_without_conviction, signingCertificateName);
                containsBulletins = false;
            }
            else
            {
                contentCertificate = await CreatePdf(certificate.Id, checkUrl, JasperReportsNames.Certificate_with_conviction, signingCertificateName);
                containsBulletins = true;
            }
            bool isExistingDoc = false;
            bool isExistingContent = false;
            DDocument doc;
            if (!string.IsNullOrEmpty(certificate.DocId))
            {
                var currentDocument = await dbContext.DDocuments.FirstOrDefaultAsync(d => d.Id == certificate.DocId);
                if (currentDocument != null)
                {
                    doc = currentDocument;
                    isExistingDoc = true;
                }
                else
                {
                    doc = new DDocument();
                    doc.Id = BaseEntity.GenerateNewId();
                    doc.Name = "Свидетелство за съдимост";
                }

            }
            else
            {
                doc = new DDocument();
                doc.Id = BaseEntity.GenerateNewId();
                doc.Name = "Свидетелство за съдимост";
            }

            DDocContent content;
            if (!isExistingDoc || doc.DocContentId == null)
            {
                content = new DDocContent();
                content.Id = BaseEntity.GenerateNewId();

            }
            else
            {
                var currentContent = await dbContext.DDocContents.FirstOrDefaultAsync(d => d.Id == doc.DocContentId);
                if (currentContent != null)
                {
                    content = currentContent;
                    isExistingContent = true;
                }
                else
                {
                    content = new DDocContent();
                    content.Id = BaseEntity.GenerateNewId();
                }

            }
            content.MimeType = "application/pdf";
            content.Content = contentCertificate;
            content.Bytes = content.Content.Length;


            doc.DocContentId = content.Id;
            doc.DocContent = content;

            certificate.DocId = doc.Id;
            if (certificate.Application.ApplicationTypeId != ApplicationConstants.ApplicationTypes.InternalCode5)
            {
                //ako не е електронно -> за печат
                certificate.StatusCode = statusCodeCertificatePaperPrint;

            }
            else
            {
                if (containsBulletins || certificate.Application.PurposeNavigation.ForSecondSignature == true)
                {
                    //ако е електронно и е за чужбина или има присъди, трябва съдия да го подпише електронно
                    certificate.StatusCode = statusCodeCertificateServerSign;
                }
                else
                {
                    //todo: за доставка
                    certificate.StatusCode = statusCodeCertificateForDelivery;

                    await DeliverCertificateAsync(certificate, mailBodyPattern,mailSubjectPattern,webportalUrl);                 

                }
            }

            if (isExistingContent)
            {
                dbContext.DDocContents.Update(content);
            }
            else
            {
                dbContext.DDocContents.Add(content);
            }
            if (isExistingDoc)
            {
                dbContext.DDocuments.Update(doc);
            }
            else
            {
                dbContext.DDocuments.Add(doc);
            }
            dbContext.ACertificates.Update(certificate);

            return contentCertificate;

        }

        public async Task DeliverCertificateAsync(ACertificate certificate, string mailBodyPattern, string mailSubjectPattern,string webportalUrl)
        {
            if (certificate.Application.SrvcResRcptMeth?.Code == ApplicationConstants.ReceivingMethods.EDelivery)
            {
                EEdeliveryMsg msg = new EEdeliveryMsg();
                msg.Id = BaseEntity.GenerateNewId();
                msg.EmailAddress = certificate.Application.Email;
                msg.CertificateId = certificate.Id;
                msg.Certificate = certificate;
                dbContext.EEdeliveryMsgs.Add(msg);
            }

            EEmailEvent mail = new EEmailEvent();
            mail.Id = BaseEntity.GenerateNewId();
            mail.EmailAddress = certificate.Application.Email;
            mail.Body = await GetBodyForCertificateMailAsync(certificate, mailBodyPattern, webportalUrl);
            mail.Subject = GetSubjectForCertificateMail(certificate, mailSubjectPattern);
            mail.CertificateId = certificate.Id;
            dbContext.EEmailEvents.Add(mail);
        }

        private async Task<string?> GetBodyForCertificateMailAsync(ACertificate certificate, string mailBodyPattern, string webportalUrl)
        {
            Dictionary<string, string> placeholdersAndValues = new Dictionary<string, string>();
            placeholdersAndValues.Add("application_registration_number", certificate.Application.RegistrationNumber);
            string cerificateDownloadUrl = await GetURLForAccessAsync(certificate.AccessCode1, webportalUrl); 
            placeholdersAndValues.Add("certificate_download_url", cerificateDownloadUrl);
            return ReplaceTextInTemplate(mailBodyPattern, placeholdersAndValues);

        }

        private string? GetSubjectForCertificateMail(ACertificate certificate, string mailSubjectPattern)
        {
            Dictionary<string, string> placeholdersAndValues = new Dictionary<string, string>();

            placeholdersAndValues.Add("application_registration_number", certificate.Application.RegistrationNumber);

            return ReplaceTextInTemplate(mailSubjectPattern, placeholdersAndValues);
        }

        private async Task<byte[]> CreatePdf(string certificateID, string checkUrl, JasperReportsNames reportName, string signingCertificateName)
        {
            byte[] fileArray = await _printerService.PrintCertificate(certificateID,checkUrl,reportName);
            //todo: кои полета да се добавят за валидиране?!
            fileArray = _pdfSignerService.SignPdf(fileArray, signingCertificateName, 
                new Dictionary<string, string>() { { "certificate_id", certificateID} });

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

        private async Task<string> GetURLForAccessAsync(string? accessCode1, string webportalUrl)
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
            return $"{url}/{CertificateConstants.UrlsInPublicSites.GET_CERTIFICATE_URL}/{accessCode1}";
        }
        public async Task<string?> GetWebPortalAddress()
        {
            return (await dbContext.GSystemParameters.FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.WEB_PORTAL_URL))?.ValueString;
        }

        private string ReplaceTextInTemplate(string htmlCode, Dictionary<string, string> placeholdersAndValues)
        {

            if (placeholdersAndValues == null)
            {
                return htmlCode;
            }

            foreach (var placeHolder in placeholdersAndValues)
            {
                string placeholderName = string.Format("[{0}]", placeHolder.Key);
                htmlCode = htmlCode.Replace(placeholderName, placeHolder.Value);
            }
            return htmlCode;
        }



    }
}
