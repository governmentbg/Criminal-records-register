namespace MJ_CAIS.DTO.BulletinAdministration
{
    public class BulletinAdministrationDTO : BaseDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? CsAuthorityName { get; set; }
        public string? StatusName { get; set; }
        public string? StatusId { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string? EcrisConvictionId { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? BulletinTypeName { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public DateTime? BulletinCreateDate { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? Ln { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
