using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WStatusH : BaseEntity
    {
        public string? Descr { get; set; }
        public decimal? ReportOrder { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string? StatusCode { get; set; }
        public string? ApplicationId { get; set; }

        public virtual WApplicationStatus? StatusCodeNavigation { get; set; }
    }
}
