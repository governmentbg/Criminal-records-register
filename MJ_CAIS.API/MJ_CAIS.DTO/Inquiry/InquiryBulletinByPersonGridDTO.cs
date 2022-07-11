namespace MJ_CAIS.DTO.Inquiry
{
    public class InquiryBulletinByPersonGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public string? Fullname { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public string? Egn { get; set; }
    }
}
