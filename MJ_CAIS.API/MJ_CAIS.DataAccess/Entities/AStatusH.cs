using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AStatusH : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Descr { get; set; }
        public decimal? ReportOrder { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? ApplicationId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CertificateId { get; set; }

        public virtual AApplication? Application { get; set; }
        public virtual ACertificate? Certificate { get; set; }
        public virtual AApplicationStatus StatusCodeNavigation { get; set; } = null!;
    }
}
