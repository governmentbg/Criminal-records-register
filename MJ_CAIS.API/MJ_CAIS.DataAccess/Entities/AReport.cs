using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AReport : BaseEntity, IBaseIdEntity
    {
        public AReport()
        {
            ARepBulletins = new HashSet<ARepBulletin>();
            AReportStatusHes = new HashSet<AReportStatusH>();
        }

        public string Id { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? DocId { get; set; }
        public string? StatusCode { get; set; }
        public string ARepApplId { get; set; } = null!;

        public virtual AReportApplication ARepAppl { get; set; } = null!;
        public virtual DDocument? Doc { get; set; }
        public virtual GUser? FirstSigner { get; set; }
        public virtual GUser? SecondSigner { get; set; }
        public virtual ICollection<ARepBulletin> ARepBulletins { get; set; }
        public virtual ICollection<AReportStatusH> AReportStatusHes { get; set; }
    }
}
