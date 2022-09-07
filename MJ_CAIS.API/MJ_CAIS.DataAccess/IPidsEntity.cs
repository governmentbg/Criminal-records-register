namespace MJ_CAIS.DataAccess
{
    public interface IPidsEntity : IBaseIdEntity
    {
        public string? Egn { get; set; }
        public string? EgnId { get; set; }
        public string? Lnch { get; set; }
        public string? LnchId { get; set; }
        public string? Ln { get; set; }
        public string? LnId { get; set; }
        public string? Suid { get; set; }
        public string? SuidId { get; set; }
    }
}
