namespace MJ_CAIS.DTO.Bulletin
{
    public class DocumentDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public string? DocTypeId { get; set; }
        public string? DocTypeName { get; set; }
        public byte[]? DocumentContent { get; set; }
        public string? DocumentContentId { get; set; }
        public string? MimeType { get; set; }
    }
}