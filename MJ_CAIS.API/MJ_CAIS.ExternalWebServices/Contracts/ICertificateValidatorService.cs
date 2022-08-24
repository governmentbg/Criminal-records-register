namespace MJ_CAIS.ExternalWebServices
{
    public interface ICertificateValidatorService
    {
        Task<bool> ValidatePdf(byte[] pdfBytes, string validationString, string certificateID);
        Task<byte[]> GetPdfForDownload(string certificateID);
        Task<byte[]> GetUnsignedPdfForDownload(string certificateID);
        Task<bool> ValidatePdf(byte[] pdfBytes, string certificateID);
    }
}