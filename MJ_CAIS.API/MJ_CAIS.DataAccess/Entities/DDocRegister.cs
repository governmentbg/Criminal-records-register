using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DDocRegister : BaseEntity, IBaseIdEntity
    {
        public DDocRegister()
        {
            DCsDocRegisters = new HashSet<DCsDocRegister>();
        }
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public decimal? RegisterIndex { get; set; }
        public bool? IsLocal { get; set; }
        public string? AppTypeId { get; set; }
        public string? RegisterTypeId { get; set; }
        public decimal? Year { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual AApplicationType? AppType { get; set; }
        public virtual DRegisterType? RegisterType { get; set; }
        public virtual ICollection<DCsDocRegister> DCsDocRegisters { get; set; }
    }
}
