namespace MJ_CAIS.DTO.Application.Public
{
    public class PublicApplicationGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public string? ApplicantName { get; set; }
        public string? PurposeId { get; set; }
        public string? Purpose { get; set; }
        public string? PurposeCountry { get; set; }
        public string? PurposePosition { get; set; }
    }
}
