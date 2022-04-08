namespace MJ_CAIS.DTO.NomenclatureDetail
{
    public class CountryDTO : BaseDTO
    {
        public string? Iso31662Code { get; set; }
        public bool? UsedForNationality { get; set; }
        public string? Remark { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}