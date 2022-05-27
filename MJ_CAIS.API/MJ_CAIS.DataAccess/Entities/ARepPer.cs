using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ARepPer : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Pid { get; set; }
        public string? PidType { get; set; }
        public string? ReportId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual AReportSearchPer? Report { get; set; }
    }
}
