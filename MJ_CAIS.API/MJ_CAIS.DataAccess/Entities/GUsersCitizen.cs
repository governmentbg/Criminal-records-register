using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUsersCitizen : BaseEntity, IBaseIdEntity
    {
        public GUsersCitizen()
        {
            WApplications = new HashSet<WApplication>();
        }
        public string Id { get; set; } = null!;
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
