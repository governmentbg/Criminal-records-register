namespace MJ_CAIS.DTO.UserExternal
{
    public class UserExternalGridDTO : BaseGridDTO
    {
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string? AdministrationName { get; set; }
        public bool? Active { get; set; }
        public string? Position { get; set; }
        public bool HasRegRegCertSubject { get; set; }
        public string? UserName { get; set; }
        public bool HasUserName { get; set; }
        public string? Remarks { get; set; }
        public bool? Denied { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool Locked
        {
            get
            {
                return LockoutEndDateUtc.HasValue && LockoutEndDateUtc.Value > DateTime.UtcNow;
            }
        }
    }
}
