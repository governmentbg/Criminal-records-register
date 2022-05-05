using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DRegisterType : BaseEntity
    {
        public DRegisterType()
        {
            DDocRegisters = new HashSet<DDocRegister>();
        }

        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<DDocRegister> DDocRegisters { get; set; }
    }
}
