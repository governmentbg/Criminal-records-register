namespace MJ_CAIS.DTO.Fbbc
{
    public class FbbcByPersonIdDTO
    {
        public string Id { get; set; }
        public string DocTypeId { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string CountryId { get; set; }
        public string Egn { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
