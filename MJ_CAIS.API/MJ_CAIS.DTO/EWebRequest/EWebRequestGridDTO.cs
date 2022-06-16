namespace MJ_CAIS.DTO.EWebRequest
{
    public class EWebRequestGridDTO : BaseGridDTO
    {
        public string Id { get; set; } = null!;
        public string WebServiceName { get; set; }
        public string WebServiceXslt { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
        public string? ResponseXml { get; set; }
        public string? ApplicationId { get; set; }
    }
}
