namespace MJ_CAIS.DTO.BulletinEvent
{
    public class BulletinEventGridDTO : BaseDTO
    {
        public string? EventType { get; set; }
        public string? StatusName { get; set; }
        public string? StatusCode { get; set; }   
        public DateTime? CreatedOn { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? BulletinId { get; set; }
        public string? Identifier { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PersonName { get; set; }
        public string? Description { get; set; }
    }
}
