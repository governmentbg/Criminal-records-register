namespace MJ_CAIS.DTO.WApplicaiton
{
    public class WApplicaitonGridDTO : BaseGridDTO
    {
        public string RegistrationNumber { get; set; }
        public string Egn { get; set; }
        public string Purpose { get; set; }
        public string PurposeDesc { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
