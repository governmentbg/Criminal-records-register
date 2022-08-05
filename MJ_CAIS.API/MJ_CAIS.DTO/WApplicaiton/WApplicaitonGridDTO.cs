namespace MJ_CAIS.DTO.WApplicaiton
{
    public class WApplicaitonGridDTO : BaseGridDTO
    {
        public string RegistrationNumber { get; set; }
        public string Egn { get; set; }
        public string Purpose { get; set; }
        public string PurposeDesc { get; set; }
        public string PaymentMethodName { get; set; }
        public string StatusName { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
    }
}
