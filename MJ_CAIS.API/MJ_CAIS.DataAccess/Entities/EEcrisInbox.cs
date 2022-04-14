using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisInbox : BaseEntity
    {
        public string Status { get; set; } = null!;
        public string XmlMessage { get; set; } = null!;
        public string XmlMessageTraits { get; set; } = null!;
        public DateTime ImportedOn { get; set; }
        public string? EcrisMsgId { get; set; }

        public virtual EEcrisMessage? EcrisMsg { get; set; }
    }
}
