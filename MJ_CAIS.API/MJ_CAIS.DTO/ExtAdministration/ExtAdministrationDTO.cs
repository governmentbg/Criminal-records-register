namespace MJ_CAIS.DTO.ExtAdministration
{
    public class ExtAdministrationDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public string? Role { get; set; }

        public List<ExtAdministrationUicDTO> ExtAdministrationUics { get; set; }
    }
}
