namespace MJ_CAIS.DTO.Role
{
    public class RoleGridDTO : BaseGridDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
