using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DCsDocRegister : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public decimal? RegisterIndex { get; set; }
        public string? CsAuthorityId { get; set; }
        public string? DocRegisterId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual DDocRegister? DocRegister { get; set; }
    }
}
