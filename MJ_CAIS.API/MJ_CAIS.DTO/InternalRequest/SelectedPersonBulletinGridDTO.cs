namespace MJ_CAIS.DTO.InternalRequest
{
    public class SelectedPersonBulletinGridDTO : BaseGridDTO
    {
        public string? BulletinId { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public string? BulletinAuthorityId { get; set; }
        public string? BulletinType { get; set; }
        public string? Remarks { get; set; }
    }
}
