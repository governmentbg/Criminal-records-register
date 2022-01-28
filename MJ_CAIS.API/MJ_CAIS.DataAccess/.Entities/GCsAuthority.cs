using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCsAuthority
    {
        public GCsAuthority()
        {
            BBulletins = new HashSet<BBulletin>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
    }
}
