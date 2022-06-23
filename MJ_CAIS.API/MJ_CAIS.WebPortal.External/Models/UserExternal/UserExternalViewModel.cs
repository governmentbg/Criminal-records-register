using MJ_CAIS.DTO.UserExternal;

namespace MJ_CAIS.WebPortal.External.Models.UserExternal
{
    public class UserExternalViewModel
    {
        public UserExternalViewModel()
        {
            this.Users = new List<UserExternalGridDTO>().AsQueryable();
        }

        public IQueryable<UserExternalGridDTO> Users { get; set; }
    }
}
