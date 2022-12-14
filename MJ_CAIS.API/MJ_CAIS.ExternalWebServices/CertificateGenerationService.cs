using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using System.Text;
using TL.Signer;

namespace MJ_CAIS.ExternalWebServices
{
    public class CertificateGenerationService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateGenerationService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IPdfSigner _pdfSignerService;
        private readonly IPrintDocumentService _printerService;
        private readonly ICertificateService _certificateService;
        private readonly ILogger<CertificateGenerationService> _logger;

        const string CERTIFICATE = "CERT";
        const string ELECTRONIC_CERTIFICATE = "ECERT";
        const string EXTERNAL_ELECTRONIC_CERTIFICATE = "EECERT";

        public CertificateGenerationService(IMapper mapper, ICertificateRepository certificateRepository,
            IPdfSigner pdfSignerService, IPrintDocumentService printerService, ICertificateService certificateService, ILogger<CertificateGenerationService> logger)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _pdfSignerService = pdfSignerService;
            _printerService = printerService;
            _certificateService = certificateService;
            _logger = logger;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public async Task<byte[]> CreateCertificate(string certificateID)
        {
            ACertificate certificate = await _certificateRepository.GetCertificateWithIncludedDataForApplicationAndBulletins(certificateID);
            if (certificate == null)
            {
                //todo: resources and EH
                throw new Exception($"Certificate with ID {certificateID} does not exist.");
            }

            var systemParameters = await (await _certificateRepository.FindAsync<GSystemParameter>(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
                                                || x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME)).ToListAsync();

            var signingCertificateName = (systemParameters.FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME))?.ValueString;



            // (await dbContext.GSystemParameters
            //                        .FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME))?.ValueString;
            if (string.IsNullOrEmpty(signingCertificateName))
            {//todo: EH & resources
                throw new Exception($"Системният параметър {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME} не е настроен.");
            }
            var statuses = await (await _certificateRepository.FindAsync<AApplicationStatus>(x => x.Code == ApplicationConstants.ApplicationStatuses.CertificateUserSign
             || x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery
             || x.Code == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint
             || x.Code == ApplicationConstants.ApplicationStatuses.Delivered)).ToListAsync();


            //     await dbContext.AApplicationStatuses.Where(x => x.Code == ApplicationConstants.ApplicationStatuses.CertificateServerSign
            //|| x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery
            //|| x.Code == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint
            //|| x.Code == ApplicationConstants.ApplicationStatuses.Delivered).ToListAsync();
            if (statuses.Count() != 4)
            {
                throw new Exception($"Няма въведени статуси: {ApplicationConstants.ApplicationStatuses.CertificateUserSign}, { ApplicationConstants.ApplicationStatuses.CertificateForDelivery}, {ApplicationConstants.ApplicationStatuses.CertificatePaperPrint}");
            }
            var statusCertificateUserSign = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.CertificateUserSign);
            var statusForDelivery = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
            var statusCertificatePaperprint = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint);
            var statusCertificateDelivered = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.Delivered);
            //todo:get patterns for mail if needed:




            string mailSubjectPattern = null;
            string mailBodyPattern = null;
            if (certificate.Application.ApplicationType.Code == ApplicationConstants.ApplicationTypes.WebExternalCertificate ||

                certificate.Application.ApplicationType.Code == ApplicationConstants.ApplicationTypes.WebCertificate)
            {
                try
                {
                    mailSubjectPattern = MailResources.DELIVERY_MAIL_SUBJECT; 
                    mailBodyPattern = MailResources.DELIVERY_MAIL_BODY;
                }
                catch (Exception ex)
                {
                    //todo: log
                }
            }
            var content = await CreateCertificate (certificate, mailSubjectPattern, mailBodyPattern, signingCertificateName, statusCertificateUserSign, statusForDelivery, statusCertificateDelivered, statusCertificatePaperprint, await GetWebPortalAddress());

            await _certificateRepository.SaveChangesAsync();
            return content;
        }



        public async Task<byte[]> GetCertificateContentAsync(string certificateID)
        {
            ACertificate content = await _certificateRepository.GetCertificateDataWithContentAndType(certificateID);
            if (content?.Doc?.DocContent?.Content == null)
            {
                throw new Exception("Content is empty");
            }
            if (content?.Doc?.DocType?.Xslt == null)
            {
                throw new Exception("Xslt is empty");
            }
            string xml = Encoding.UTF8.GetString(content.Doc.DocContent.Content);
            var html = XmlUtils.XmlTransform(content.Doc.DocType.Xslt, xml);
            var result = Encoding.UTF8.GetBytes(html);

            return await Task.FromResult(result);
        }

        public async Task<byte[]> GetArchiveDocumentContentAsync(string id)
        {
            var document = await _certificateRepository.GetArchiveDocumentContentAsync(id);
            if (document == null || document.Content == null) return null;
            if (document.Xslt == null) return document.Content;

            var xml = Encoding.UTF8.GetString(document.Content);
            var html = XmlUtils.XmlTransform(document.Xslt, xml);
            var result = Encoding.UTF8.GetBytes(html);

            return result;
        }

        public async Task<byte[]> CreateCertificate(ACertificate certificate, string mailSubjectPattern,
            string mailBodyPattern, string signingCertificateName, AApplicationStatus statusCertificateUserSign, AApplicationStatus statusCertificateForDelivery, AApplicationStatus statusCertificateDelivered, AApplicationStatus statusCertificatePaperPrint, string? webportalUrl = null)
        {
            //целта може да е null -> тогава важи за всички; при null изпраща се директно, а не за подпис от съдия
            if (certificate.Application.PurposeNavigation == null)
            {
                throw new BusinessLogicException("Полето 'Цел' е празно");
            }
            var docTypeID = (await _certificateRepository.SingleOrDefaultAsync<DDocType>(x => x.Code == DocumentTypesConstants.DocumentTypesCodes.ConvictionCertificate))?.Id;
            if (string.IsNullOrEmpty(docTypeID))
            {
                throw new BusinessLogicException($"Не съществува тип документ с код {DocumentTypesConstants.DocumentTypesCodes.ConvictionCertificate}");
            }
            
            byte[] contentCertificate;
            string checkUrl = await GetURLForAccessAsync(certificate.AccessCode1, webportalUrl);
            bool containsBulletins = certificate.AAppBulletins.Where(aa => aa.Approved == true).Count() != 0;
            _logger.LogTrace($"{certificate.Id}: Before CreatePdf. ");
            switch (certificate.Application.ApplicationType.Code)
            {
                case ApplicationConstants.ApplicationTypes.WebExternalCertificate:
                    contentCertificate = await CreatePdf(certificate.Id, checkUrl, EXTERNAL_ELECTRONIC_CERTIFICATE, signingCertificateName);
                    break;
                case ApplicationConstants.ApplicationTypes.WebCertificate:
                    contentCertificate = await CreatePdf(certificate.Id, checkUrl, ELECTRONIC_CERTIFICATE, signingCertificateName);
                    break;
                default:
                    contentCertificate = await CreatePdf(certificate.Id, checkUrl, CERTIFICATE, signingCertificateName);
                    break;
            }
            _logger.LogTrace($"{certificate.Id}: After CreatePdf. ");
            bool isExistingDoc = false;
            bool isExistingContent = false;
            DDocument doc;
            if (!string.IsNullOrEmpty(certificate.DocId))
            {
                var currentDocument = await _certificateRepository.SingleOrDefaultAsync<DDocument>(d => d.Id == certificate.DocId);
                //await dbContext.DDocuments.FirstOrDefaultAsync(d => d.Id == certificate.DocId);
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
            doc.DocTypeId = docTypeID;
            DDocContent content;
            if (!isExistingDoc || doc.DocContentId == null)
            {
                content = new DDocContent();
                content.Id = BaseEntity.GenerateNewId();

            }
            else
            {
                var currentContent = await _certificateRepository.SingleOrDefaultAsync<DDocContent>(d => d.Id == doc.DocContentId);
                //await dbContext.DDocContents.FirstOrDefaultAsync(d => d.Id == doc.DocContentId);
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
            certificate.Doc = doc;

            if (isExistingContent)
            {
                content.EntityState = EntityStateEnum.Modified;
                if (content.ModifiedProperties == null)
                {
                    content.ModifiedProperties = new List<string>();
                }
                content.ModifiedProperties.Add(nameof(content.MimeType));
                content.ModifiedProperties.Add(nameof(content.Content));
                content.ModifiedProperties.Add(nameof(content.Bytes));

                //dbContext.DDocContents.Update(content);
            }
            else
            {
                content.EntityState = EntityStateEnum.Added;
                //dbContext.DDocContents.Add(content);
            }
            if (isExistingDoc)
            {
                doc.EntityState = EntityStateEnum.Modified;
                if (doc.ModifiedProperties == null)
                {
                    doc.ModifiedProperties = new List<string>();
                }
                doc.ModifiedProperties.Add(nameof(doc.DocContentId));
                doc.ModifiedProperties.Add(nameof(doc.DocTypeId));
                // dbContext.DDocuments.Update(doc);
            }
            else
            {
                doc.EntityState = EntityStateEnum.Added;
                //dbContext.DDocuments.Add(doc);
            }
            //dbContext.ACertificates.Update(certificate);
            certificate.EntityState = EntityStateEnum.Modified;


            if (certificate.ModifiedProperties == null)
            {
                certificate.ModifiedProperties = new List<string>();
            }

            certificate.ModifiedProperties.Add(nameof(certificate.DocId));

            if (certificate.Application.ApplicationTypeId != ApplicationConstants.ApplicationTypes.WebExternalCertificate
                && certificate.Application.ApplicationTypeId != ApplicationConstants.ApplicationTypes.WebCertificate)
            {
                //ako не е електронно -> за печат
                await _certificateService.SetCertificateStatus(certificate, statusCertificatePaperPrint, "За отпечатване");
                // certificate.StatusCode = statusCodeCertificatePaperPrint;

            }
            else
            {
                var citizenships = await _certificateRepository.FindAsync<AAppCitizenship>(cit => cit.ApplicationId == certificate.ApplicationId
                && cit.CountryId==GlobalConstants.BGCountryId);
                if (containsBulletins || (certificate.Application.PurposeNavigation != null && certificate.Application.PurposeNavigation.ForSecondSignature == true)
                    || (citizenships==null || citizenships.Count() == 0))
                {
                    //ако е електронно и е за чужбина или има присъди, трябва съдия да го подпише електронно
                    await _certificateService.SetCertificateStatus(certificate, statusCertificateUserSign, "За подпис от юрист");

                }
                else
                {

                    try
                    {
                        if (!string.IsNullOrEmpty(mailSubjectPattern) && !string.IsNullOrEmpty(mailBodyPattern))
                        {
                            await DeliverCertificateAsync(certificate, mailBodyPattern, mailSubjectPattern, webportalUrl);
                            await _certificateService.SetCertificateStatus(certificate, statusCertificateDelivered, "Доставено");
                        }
                        else
                        {
                            await _certificateService.SetCertificateStatus(certificate, statusCertificateForDelivery, "За доставяне");
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        await _certificateService.SetCertificateStatus(certificate, statusCertificateForDelivery, "За доставяне");
                    }
                }
            }
            if (!certificate.ModifiedProperties.Contains(nameof(certificate.StatusCode)))
            {
                certificate.ModifiedProperties.Add(nameof(certificate.StatusCode));
            }
            _certificateRepository.ApplyChanges(content, new List<IBaseIdEntity>());
            _certificateRepository.ApplyChanges(doc, new List<IBaseIdEntity>());
            _certificateRepository.ApplyChanges(certificate, new List<IBaseIdEntity>());
            _logger.LogTrace($"{certificate.Id}: Exit method. ");
            return contentCertificate;

        }

        public async Task DeliverCertificateAsync(ACertificate certificate, string mailBodyPattern, string mailSubjectPattern, string webportalUrl)
        {
            if (certificate.Application.SrvcResRcptMeth?.Code == ApplicationConstants.ReceivingMethods.EDelivery)
            {
                EEdeliveryMsg msg = new EEdeliveryMsg();
                msg.Id = BaseEntity.GenerateNewId();
                msg.EmailAddress = certificate.Application.Email;
                msg.CertificateId = certificate.Id;
               // msg.Certificate = certificate;
                msg.Status = EdeliveryConstants.EdeliveryStatuses.Pending;
                msg.EntityState = EntityStateEnum.Added;
                _certificateRepository.ApplyChanges(msg, new List<IBaseIdEntity>());
                // dbContext.EEdeliveryMsgs.Add(msg);
            }

            EEmailEvent mail = new EEmailEvent();
            mail.Id = BaseEntity.GenerateNewId();
            mail.EmailAddress = certificate.Application.Email;
            mail.Body = await GetBodyForCertificateMailAsync(certificate, mailBodyPattern, webportalUrl);
            mail.Subject = GetSubjectForCertificateMail(certificate, mailSubjectPattern);
            mail.EmailStatus = EmailStatusConstants.Pending;
            mail.CertificateId = certificate.Id;
            mail.EntityState = EntityStateEnum.Added;
            _certificateRepository.ApplyChanges(mail, new List<IBaseIdEntity>());
            //dbContext.EEmailEvents.Add(mail);
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

        private async Task<byte[]> CreatePdf(string certificateID, string checkUrl, string certType, string signingCertificateName)
        {
            byte[] fileArray = null;
            switch (certType)
            {
                case CERTIFICATE:
                    fileArray = await _printerService.PrintCertificate(certificateID, checkUrl);
                    break;
                case ELECTRONIC_CERTIFICATE:
                    fileArray = await _printerService.PrintElectronicCertificate(certificateID, checkUrl);
                    break;
                case EXTERNAL_ELECTRONIC_CERTIFICATE:
                    fileArray = await _printerService.PrintExternalElectronicCertificate(certificateID, checkUrl);
                    break;
            }

            //todo: кои полета да се добавят за валидиране?!
            fileArray = _pdfSignerService.SignPdf(fileArray, signingCertificateName, null);

            return fileArray;

        }

        //private string GetUrlOfCertificateReport(JasperReportsNames reportName)
        //{

        //    return $"{CertificateConstants.UrlsInJasper.REPORTS_URL}/{reportName}";
        //}



        //private async Task<string> GetURLForQRCodeAsync(string? accessCode1, string webportalUrl)
        //{
        //    var url = webportalUrl;
        //    if (string.IsNullOrEmpty(webportalUrl))
        //    {
        //        url = await GetWebPortalAddress();
        //        if (url == null)
        //        {
        //            //todo:resources & EH
        //            throw new Exception("Web portal URL is not set.");
        //        }
        //    }
        //    return $"{url}/{CertificateConstants.UrlsInPublicSites.VIEW_CERTIFICATE_URL}/{accessCode1}";
        //}

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
            var urlReturn = new Uri(new Uri(url), CertificateConstants.UrlsInPublicSites.GET_CERTIFICATE_URL).ToString();
            urlReturn = urlReturn + "?Id=" + accessCode1;
            return urlReturn;

        }
        public async Task<string?> GetWebPortalAddress()
        {
            return (await _certificateRepository.SingleOrDefaultAsync<GSystemParameter>(x => x.Code == SystemParametersConstants.SystemParametersNames.WEB_PORTAL_URL))?.ValueString;
            //(await dbContext.GSystemParameters.FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.WEB_PORTAL_URL))?.ValueString;
        }

        private string ReplaceTextInTemplate(string htmlCode, Dictionary<string, string> placeholdersAndValues)
        {

            if (placeholdersAndValues == null)
            {
                return htmlCode;
            }

            foreach (var placeHolder in placeholdersAndValues)
            {
                string placeholderName = $"[{{{placeHolder.Key}}}]";
                htmlCode = htmlCode.Replace(placeholderName, placeHolder.Value);
            }
            return htmlCode;
        }



    }
}
