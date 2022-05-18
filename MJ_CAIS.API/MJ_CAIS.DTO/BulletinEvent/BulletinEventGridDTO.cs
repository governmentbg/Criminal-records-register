namespace MJ_CAIS.DTO.BulletinEvent
{
    public class BulletinEventGridDTO : BaseDTO
    {
        public string? EventType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? Ln { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PersonName { get; set; }
        public string? Description { get; set; }
    }
}
