using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AAppBulletin : BaseEntity
    {
        public string? BulletinId { get; set; }
        public bool? Approved { get; set; }
        public string? ConvictionText { get; set; }
        public decimal? OrderNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string? CertificateId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual ACertificate? Certificate { get; set; }
    }
}
