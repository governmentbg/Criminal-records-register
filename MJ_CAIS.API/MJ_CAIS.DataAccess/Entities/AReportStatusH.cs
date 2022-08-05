using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AReportStatusH : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Descr { get; set; }
        public decimal? ReportOrder { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? AReportApplId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? AReportId { get; set; }

        public virtual AReport? AReport { get; set; }
        public virtual AReportApplication? AReportAppl { get; set; }
        public virtual AReportStatus StatusCodeNavigation { get; set; } = null!;
    }
}
