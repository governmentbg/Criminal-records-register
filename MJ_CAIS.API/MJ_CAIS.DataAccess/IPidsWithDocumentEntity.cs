namespace MJ_CAIS.DataAccess
{
    public interface IPidsWithDocumentEntity : IPidsEntity
    {
        public string? IdDocNumber { get; set; }
        public string? IdDocNumberId { get; set; }
    }
}
