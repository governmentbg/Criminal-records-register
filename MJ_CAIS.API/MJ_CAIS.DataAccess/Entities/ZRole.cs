using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZRole
    {
        public ZRole()
        {
            ZUserRoles = new HashSet<ZUserRole>();
        }

        public decimal RoleId { get; set; }
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Description { get; set; }
        public string RolesPk { get; set; } = null!;

        public virtual ICollection<ZUserRole> ZUserRoles { get; set; }
    }
}
