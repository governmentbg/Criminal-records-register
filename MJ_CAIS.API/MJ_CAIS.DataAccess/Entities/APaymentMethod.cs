using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class APaymentMethod : BaseEntity
    {
        public APaymentMethod()
        {
            AApplications = new HashSet<AApplication>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? IsForWeb { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
    }
}
