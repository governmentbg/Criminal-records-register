using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BEventStatus
    {
        public BEventStatus()
        {
            BBulEvents = new HashSet<BBulEvent>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<BBulEvent> BBulEvents { get; set; }
    }
}
