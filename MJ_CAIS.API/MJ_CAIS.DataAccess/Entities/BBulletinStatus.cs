using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BBulletinStatus
    {
        public BBulletinStatus()
        {
            BBulletins = new HashSet<BBulletin>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
    }
}
