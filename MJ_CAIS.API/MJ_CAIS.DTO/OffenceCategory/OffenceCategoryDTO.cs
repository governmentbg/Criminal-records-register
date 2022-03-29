namespace MJ_CAIS.DTO.OffenceCategory
{
    public class OffenceCategoryDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal? OffLevel { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? OrderNumber { get; set; }
    }
}
