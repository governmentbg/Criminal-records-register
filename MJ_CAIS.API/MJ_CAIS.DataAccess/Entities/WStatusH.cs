using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WStatusH : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Descr { get; set; }
        public decimal? ReportOrder { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? StatusCode { get; set; }
        public string? ApplicationId { get; set; }

        public virtual WApplicationStatus? StatusCodeNavigation { get; set; }
    }
}
