namespace MJ_CAIS.DTO.Certificate
{
    public class BulletinCheckDTO : BaseDTO
    {
        public string BulletinId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? CaseNumber { get; set; }
        public string? BulletinAuthorityId { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public string? BulletinTypeName { get; set; }
        public string? BulletinDecisionNumber { get; set; }
        public DateTime? BulletinDecisionDate { get; set; }


    }
}
