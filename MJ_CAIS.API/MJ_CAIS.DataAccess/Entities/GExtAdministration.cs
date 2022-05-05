using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GExtAdministration : BaseEntity
    {
        public GExtAdministration()
        {
            GUsersExts = new HashSet<GUsersExt>();
        }

        public string? Name { get; set; }
        public string? Descr { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<GUsersExt> GUsersExts { get; set; }
    }
}
