namespace MJ_CAIS.DTO.AStatusH
{
    public class AStatusHGridDTO 
    {
        public string Id { get; set; } = null!;
        public string? Descr { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string StatusCode { get; set; } = null!;
      
    }
}
