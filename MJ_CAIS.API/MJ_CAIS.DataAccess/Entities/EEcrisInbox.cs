using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisInbox : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string XmlMessage { get; set; } = null!;
        public string XmlMessageTraits { get; set; } = null!;
        public DateTime ImportedOn { get; set; }
        public string? EcrisMsgId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual EEcrisMessage? EcrisMsg { get; set; }
    }
}
