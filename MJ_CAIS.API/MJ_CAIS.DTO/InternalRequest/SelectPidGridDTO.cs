namespace MJ_CAIS.DTO.InternalRequest
{
    public class SelectPidGridDTO : BaseGridDTO
    {
        public string? Pid { get; set; }
        public string? PidType { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? PersonBirthDate { get; set; }
    }
}
