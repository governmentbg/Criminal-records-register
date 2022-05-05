using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonIdType : BaseEntity
    {
        public PPersonIdType()
        {
            PPersonIds = new HashSet<PPersonId>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<PPersonId> PPersonIds { get; set; }
    }
}
