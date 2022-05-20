using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AApplicationType : BaseEntity, IBaseIdEntity
    {
        public AApplicationType()
        {
            AApplications = new HashSet<AApplication>();
            DDocRegisters = new HashSet<DDocRegister>();
            WApplications = new HashSet<WApplication>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<DDocRegister> DDocRegisters { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
