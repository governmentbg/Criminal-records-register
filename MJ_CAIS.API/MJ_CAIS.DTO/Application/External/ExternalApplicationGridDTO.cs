namespace MJ_CAIS.DTO.Application.External
{
    public class ExternalApplicationGridDTO : BaseDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public string? ApplicantName { get; set; }
        public string? PurposeId { get; set; }
        public string? Purpose { get; set; }
        public string? PurposeCountry { get; set; }
        public string? PurposePosition { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
