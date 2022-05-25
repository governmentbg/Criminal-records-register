namespace MJ_CAIS.DTO.BulletinEvent
{
    public class BulletinSancttionsEventDTO : BaseDTO
    {

        public bool? PrevSuspSent { get; set; }

        public DateTime? DecisionDate { get; set; }

        public IEnumerable<SanctionEventDTO> Sanctions { get; set; }
        
        public IEnumerable<DateTime?> OffencesEndDates { get; set; }
    }
}
