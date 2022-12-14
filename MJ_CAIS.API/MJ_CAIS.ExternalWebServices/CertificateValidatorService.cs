using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.DataAccess;
using TL.Signer;
using TL.Signer.Models;

namespace MJ_CAIS.ExternalWebServices
{
    public class CertificateValidatorService : ICertificateValidatorService
    {
        private readonly IPdfSignatureValidator _pdfSignatureValidator;
        private readonly IPdfSigner _pdfSignerService;
        private readonly CaisDbContext _dbContext;
        private readonly IUserContext _userContext;


        public CertificateValidatorService(IPdfSignatureValidator pdfSignatureValidator,
            CaisDbContext dbContext, IPdfSigner pdfSigner,    IUserContext userContext)
        {
            _pdfSignatureValidator = pdfSignatureValidator;
            _pdfSignerService = pdfSigner;
            _dbContext = dbContext;
            _userContext =userContext;
    }

        public async Task<bool> ValidatePdf(byte[] pdfBytes, string validationString, string certificateID)
        {
            // return true;
            var curentUser = await _dbContext.GUsers.AsNoTracking().FirstOrDefaultAsync(u=>u.Id== _userContext.UserId);
            if (curentUser == null || string.IsNullOrEmpty(curentUser.Egn))
            {
                throw new BusinessLogicException("Текущия потребител няма записано ЕГН.");
            }

            var egn = curentUser.Egn;

            var certNumber = _pdfSignatureValidator.GetSignaturesCount(pdfBytes);
            if (certNumber < 2)
            {
                return false;
            }
            using Stream pdfStream = new MemoryStream(pdfBytes);

            var metadata = ExternalServicesHelper.GetDictionaryMetadata(certificateID, validationString);

         

            //bool result = validator.ValidateClientSignature(fileStream, personIdentifier);
            //result &= validator.ValidateServerSignature(pdfStream, signatureName, out List<Error> errors, metadata);
            List<Error> errors = new List<Error>();
            var result = _pdfSignatureValidator.ValidateServerSignature(pdfStream, await GetSigningCertificateName(true),
             out errors, metadata, certNumber-2);

            if (errors.Count == 0)
            {
                
                result = result && _pdfSignatureValidator.ValidateClientSignature(pdfStream, egn, 0);
                return result;
            }
            else
            {
                throw new BusinessLogicException($"Брой валидационни грешки: {errors.Count()}; {string.Join(';',errors.Select(x=>x.ErrorMessage))}");
            }
        }

        public async Task<byte[]> GetPdfForDownload(string certificateID)
        {
            var contentFromDb = await GetCertificateContent(certificateID);
            return await SignPdfForDownload(contentFromDb, certificateID);
        }

        public async Task<byte[]> GetUnsignedPdfForDownload(string certificateID)
        {
            var contentFromDb = await GetCertificateContent(certificateID);
            return contentFromDb;
        }

        public async Task<bool> ValidatePdf(byte[] pdfBytes, string certificateID)
        {

            var contentFromDb = await GetCertificateContent(certificateID);
            var validationString = ExternalServicesHelper.GetValidationString(contentFromDb);

            bool result;
          
                result = await ValidatePdf(pdfBytes, validationString, certificateID);
            if (!result)
            {
                throw new BusinessLogicException("Грешка при валидация на сертификатите.");
            }

            return result;
        }

        public async Task<byte[]> SignPdfForDownload(byte[] pdfBytes, string certificateID)
        {
            using Stream pdfStream = new MemoryStream(pdfBytes);

            var metadata = ExternalServicesHelper.GetDictionaryMetadata(certificateID,
                ExternalServicesHelper.GetValidationString(pdfBytes));

            return _pdfSignerService.SignPdf(pdfBytes, await GetSigningCertificateName(true), metadata);
        }

        private async Task<string> GetSigningCertificateName(bool forDownload = false)
        {
            string systemNameOfCertificate = "";
            if (forDownload)
            {
                systemNameOfCertificate = SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME_FOR_DOWNLOAD;
            }
            else
            {
                systemNameOfCertificate = SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME;
            }
            //todo: дали да е този сертификат?!
            var signingCertificateName = (await _dbContext.GSystemParameters
                    .FirstOrDefaultAsync(x =>
                        x.Code == systemNameOfCertificate))
                ?.ValueString;
            if (string.IsNullOrEmpty(signingCertificateName))
            {
                //todo: EH & resources
                throw new Exception(
                    $"Системният параметър {systemNameOfCertificate} не е настроен.");
            }

            return signingCertificateName;
        }

        private async Task<byte[]> GetCertificateContent(string certificateID)
        {
            var content = await _dbContext.ACertificates.AsNoTracking().Where(x => x.Id == certificateID && x.Doc != null)
                .Select(x => x.Doc.DocContent).FirstOrDefaultAsync();
            if (content == null || content.Content == null || content.Content.Length == 0)
            {
                //todo: resources && EH
                throw new Exception("Не съществува pdf файл за този сертификат");
            }

            return content.Content;
        }
    }
}