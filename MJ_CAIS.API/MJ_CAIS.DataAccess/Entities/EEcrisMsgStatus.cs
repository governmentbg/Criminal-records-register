using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMsgStatus
    {
        public EEcrisMsgStatus()
        {
            EEcrisMessages = new HashSet<EEcrisMessage>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<EEcrisMessage> EEcrisMessages { get; set; }
    }
}
