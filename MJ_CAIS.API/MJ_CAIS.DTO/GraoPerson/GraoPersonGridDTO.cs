namespace MJ_CAIS.DTO.EcrisMessage
{
    public class GraoPersonGridDTO : BaseGridDTO
    {
        public string? Egn { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Sex { get; set; }
    }
}
