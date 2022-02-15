namespace MJ_CAIS.DataAccess
{
    public interface IBaseNomenclature
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }
}
