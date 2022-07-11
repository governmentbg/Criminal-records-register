namespace MJ_CAIS.DTO.Person
{
    public class PersonBulletinGridDTO : BaseGridDTO
    {
        public string? BulletinType { get; set; }
        public string? StatusName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string BulletinAuthorityName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CaseNumberAndYear { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? FullName { get; set; }
    }
}