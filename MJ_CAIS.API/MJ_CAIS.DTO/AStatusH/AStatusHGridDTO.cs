namespace MJ_CAIS.DTO.AStatusH
{
    public class AStatusHGridDTO : BaseGridDTO
    {
        public string? Descr { get; set; }
        public string? UpdatedBy { get; set; }
        public string StatusCode { get; set; } = null!;   
    }
}
