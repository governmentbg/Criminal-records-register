namespace MJ_CAIS.DTO.BulletinAdministration
{
    public class BulletinAdministrationGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public string? Egn { get; set; }
    }
}
