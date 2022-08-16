using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisCommunStatus
    {
        public EEcrisCommunStatus()
        {
            EEcrisInboxes = new HashSet<EEcrisInbox>();
            EEcrisOutboxes = new HashSet<EEcrisOutbox>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string StatusType { get; set; } = null!;
        public string? Descr { get; set; }

        public virtual ICollection<EEcrisInbox> EEcrisInboxes { get; set; }
        public virtual ICollection<EEcrisOutbox> EEcrisOutboxes { get; set; }
    }
}
