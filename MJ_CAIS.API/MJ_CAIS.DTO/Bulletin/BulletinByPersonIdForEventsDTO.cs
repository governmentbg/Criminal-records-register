namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinByPersonIdForEventsDTO : BulletinByPersonIdDTO
    {
        public DateTime? DecisionDate { get; set; }
        public bool? PrevSuspSent { get; set; }
        public decimal? Version { get; set; }
        public string CaseTypeId { get; set; }
    }
}
