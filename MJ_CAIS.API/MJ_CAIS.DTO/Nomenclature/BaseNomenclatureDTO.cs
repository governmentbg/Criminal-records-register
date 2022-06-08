namespace MJ_CAIS.DTO.Nomenclature
{
    public class BaseNomenclatureDTO : BaseGridDTO
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? NameEn { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }
}
