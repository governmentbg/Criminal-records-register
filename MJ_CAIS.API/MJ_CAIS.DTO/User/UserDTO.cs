using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.User
{
    public class UserDTO : BaseDTO
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public bool? Active { get; set; }
        public string? Email { get; set; }
        public string? Egn { get; set; }
        public string? CsAuthorityId { get; set; }
        public string? Position { get; set; }
        public MultipleChooseDTO Roles { get; set; } = new MultipleChooseDTO();
    }
}
