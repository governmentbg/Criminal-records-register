namespace MJ_CAIS.ExternalWebServices
{
    public interface ICertificateValidatorService
    {
       Task<bool> ValidatePdf(byte[] pdfBytes,  string validationString, string certificateID);
    }
}