using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonIdType : BaseEntity, IBaseIdEntity
    {
        public PPersonIdType()
        {
            PPersonIds = new HashSet<PPersonId>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<PPersonId> PPersonIds { get; set; }
    }
}
