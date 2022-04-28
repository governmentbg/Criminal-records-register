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

        public virtual ICollection<GUsersExt> GUsersExts { get; set; }
    }
}
