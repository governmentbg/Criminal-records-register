namespace MJ_CAIS.DTO.EcrisTcn
{
    public class EcrisTcnGridDTO : BaseGridDTO
    {
        public string? Action { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Status { get; set; }
        public string? Identifier { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? BulletinId { get; set; }
    }
}
