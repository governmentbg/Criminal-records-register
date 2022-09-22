using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.UserExternal
{
    public class UserExternalInDTO : BaseDTO
    {
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string? AdministrationId { get; set; }
        public bool? Active { get; set; }
        public string? Position { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public string? Uic { get; set; }
        public string? Ou { get; set; }
        public string?  Remarks { get; set; }
        public bool? Denied { get; set; }
    }
}
