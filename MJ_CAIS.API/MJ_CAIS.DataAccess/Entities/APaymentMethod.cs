using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class APaymentMethod : BaseEntity, IBaseIdEntity
    {
        public APaymentMethod()
        {
            AApplications = new HashSet<AApplication>();
            WApplications = new HashSet<WApplication>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? IsForWeb { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsForDesk { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
