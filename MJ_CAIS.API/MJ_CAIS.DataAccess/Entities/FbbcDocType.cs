using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class FbbcDocType
    {
        public FbbcDocType()
        {
            Fbbcs = new HashSet<Fbbc>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Fbbc> Fbbcs { get; set; }
    }
}
