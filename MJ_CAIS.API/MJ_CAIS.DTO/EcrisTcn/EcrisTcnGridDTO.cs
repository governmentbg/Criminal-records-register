namespace MJ_CAIS.DTO.EcrisTcn
{
    public class EcrisTcnGridDTO : BaseDTO
    {
        public string? Action { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Status { get; set; }
        public string? Identifier { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountryName { get; set; }
    }
}
