namespace MJ_CAIS.DTO.BulletinAdministration
{
    public class BulletinAdministrationGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public string? FullName { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public string? Identifier { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CaseData { get; set; }
    }
}
