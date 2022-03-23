namespace MJ_CAIS.DTO.InternalRequest
{
    public class InternalRequestGridDTO : BaseDTO
    {
        public string? RegNumber { get; set; }
        public DateTime? RequestDate { get; set; }
        public string ReqStatus { get; set; }
        public string? Description { get; set; }
        public string? BulletinNumber { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
    }
}
