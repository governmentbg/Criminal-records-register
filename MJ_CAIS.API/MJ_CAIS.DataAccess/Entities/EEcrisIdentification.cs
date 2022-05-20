using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisIdentification : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? EcrisMsgId { get; set; }
        public string? GraoPersonId { get; set; }
        public decimal? Approved { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual EEcrisMessage? EcrisMsg { get; set; }
        public virtual GraoPerson? GraoPerson { get; set; }
    }
}
