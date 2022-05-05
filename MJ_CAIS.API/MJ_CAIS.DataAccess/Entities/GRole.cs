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
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<GUserRole> GUserRoles { get; set; }
    }
}
