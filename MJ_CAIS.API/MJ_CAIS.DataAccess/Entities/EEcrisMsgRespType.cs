using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMsgRespType : BaseEntity
    {
        public EEcrisMsgRespType()
        {
            EEcrisMessages = new HashSet<EEcrisMessage>();
        }

        public string? EcrisTechnId { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? EcrisMsgTypeId { get; set; }

        public virtual EEcrisMsgType? EcrisMsgType { get; set; }
        public virtual ICollection<EEcrisMessage> EEcrisMessages { get; set; }
    }
}
