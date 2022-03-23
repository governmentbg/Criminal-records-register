using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GRole : BaseEntity
    {
        public GRole()
        {
            GUserRoles = new HashSet<GUserRole>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<GUserRole> GUserRoles { get; set; }
    }
}
