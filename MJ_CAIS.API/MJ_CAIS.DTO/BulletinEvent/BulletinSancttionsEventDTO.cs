namespace MJ_CAIS.DTO.BulletinEvent
{
    public class BulletinSancttionsEventDTO : BaseDTO
    {
        public string? StatusId { get; set; }

        public string? BulletinType { get; set; }
       
        public string? CaseType { get; set; }

        public bool? PrevSuspSent { get; set; }

        public DateTime? DecisionDate { get; set; }

    }
}
