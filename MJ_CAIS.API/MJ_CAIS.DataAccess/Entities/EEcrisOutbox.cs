using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisOutbox : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string XmlObject { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public DateTime? ExecutionDate { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
        public decimal? Attempts { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? EcrisMsgId { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual EEcrisMessage? EcrisMsg { get; set; }
    }
}
