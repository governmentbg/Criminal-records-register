namespace MJ_CAIS.DTO.Application.Public
{
    public class PublicApplicationGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public string? PurposeTypeName { get; set; }
        public string? Purpose { get; set; }
        public string? Email { get; set; }
    }
}
