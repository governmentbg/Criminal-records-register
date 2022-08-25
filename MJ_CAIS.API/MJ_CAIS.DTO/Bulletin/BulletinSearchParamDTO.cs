namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinSearchParamDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public string? StatusId { get; set; }
        public string? CaseNumber { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
