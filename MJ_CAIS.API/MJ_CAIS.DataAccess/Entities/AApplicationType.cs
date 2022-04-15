using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AApplicationType : BaseEntity
    {
        public AApplicationType()
        {
            AApplications = new HashSet<AApplication>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
    }
}
