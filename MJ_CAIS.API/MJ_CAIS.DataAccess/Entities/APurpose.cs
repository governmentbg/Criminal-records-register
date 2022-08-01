using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class APurpose : BaseEntity, IBaseIdEntity
    {
        public APurpose()
        {
            AApplications = new HashSet<AApplication>();
            AReportApplications = new HashSet<AReportApplication>();
            WApplications = new HashSet<WApplication>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? RequestInfo { get; set; }
        public bool? TaxFree { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? InstructionsForFiles { get; set; }
        public bool? ForSecondSignature { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<AReportApplication> AReportApplications { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
