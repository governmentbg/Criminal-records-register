using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class FbbcStatus
    {
        public FbbcStatus()
        {
            Fbbcs = new HashSet<Fbbc>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<Fbbc> Fbbcs { get; set; }
    }
}
