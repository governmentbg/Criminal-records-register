namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public string? FullName { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public string? Identifier { get; set; }
        public string? CaseData { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? RehabilitationDate { get; set; }
    }
}
