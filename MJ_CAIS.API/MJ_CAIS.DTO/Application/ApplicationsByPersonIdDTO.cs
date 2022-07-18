namespace MJ_CAIS.DTO.Application
{
    public class ApplicationsByPersonIdDTO
    {
        public string ApplicationId { get; set; }
        public string ApplicationTypeId { get; set; }
        public string CsAuthorityId { get; set; }
        public string ApplicantName { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Familyname { get; set; }
        public string Fullname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Egn { get; set; }
        public string Lnch { get; set; }
        public string WApplicationId { get; set; }
    }
}
