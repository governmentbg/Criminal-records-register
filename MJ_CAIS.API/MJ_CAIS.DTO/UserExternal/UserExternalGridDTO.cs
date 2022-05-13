using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.UserExternal
{
    public class UserExternalGridDTO : BaseDTO
    {
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string? AdministrationName { get; set; }
        public bool? Active { get; set; }
        public string? Position { get; set; }
    }
}
