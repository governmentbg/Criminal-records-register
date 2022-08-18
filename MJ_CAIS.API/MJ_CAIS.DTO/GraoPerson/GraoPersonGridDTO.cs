namespace MJ_CAIS.DTO.EcrisMessage
{
    public class GraoPersonGridDTO : BaseGridDTO
    {
        public string? Identifier { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Sex { get; set; }
    }
}
