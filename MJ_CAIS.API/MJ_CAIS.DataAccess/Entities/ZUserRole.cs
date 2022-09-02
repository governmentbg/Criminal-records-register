using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZUserRole
    {
        public string UserId { get; set; } = null!;
        public decimal RoleId { get; set; }
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public string UserRolesPk { get; set; } = null!;

        public virtual ZRole Role { get; set; } = null!;
        public virtual ZUser User { get; set; } = null!;
    }
}
