using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMsgType
    {
        public EEcrisMsgType()
        {
            EEcrisMessages = new HashSet<EEcrisMessage>();
            EEcrisMsgRespTypes = new HashSet<EEcrisMsgRespType>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<EEcrisMessage> EEcrisMessages { get; set; }
        public virtual ICollection<EEcrisMsgRespType> EEcrisMsgRespTypes { get; set; }
    }
}
