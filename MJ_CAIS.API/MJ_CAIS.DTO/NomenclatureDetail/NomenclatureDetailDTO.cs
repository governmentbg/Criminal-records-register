namespace MJ_CAIS.DTO.NomenclatureDetail
{
    public class NomenclatureDetailDTO : BaseDTO
    {
        public string Id { get; set; } = null!;
        public string? TableName { get; set; }
        public string? Descr { get; set; }
    }
}
