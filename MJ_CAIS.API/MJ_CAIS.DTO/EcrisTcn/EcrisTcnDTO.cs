namespace MJ_CAIS.DTO.EcrisTcn
{
    public class EcrisTcnDTO : BaseDTO
    {
        public string Id { get; set; } = null!;
        public string? Action { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Status { get; set; }
        public string? BulletinId { get; set; }
    }
}
