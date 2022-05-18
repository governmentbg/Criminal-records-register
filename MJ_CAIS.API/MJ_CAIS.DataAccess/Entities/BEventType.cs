using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BEventType
    {
        public BEventType()
        {
            BBulEvents = new HashSet<BBulEvent>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? GroupCode { get; set; }

        public virtual ICollection<BBulEvent> BBulEvents { get; set; }
    }
}
