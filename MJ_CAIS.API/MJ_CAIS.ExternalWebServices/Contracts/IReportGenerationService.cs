namespace MJ_CAIS.ExternalWebServices.Contracts
{
    public interface IReportGenerationService
    {
        Task<byte[]> CreateReport(string reportID, string firstSignerId = null, string secondSignerId = null);
    }
}
