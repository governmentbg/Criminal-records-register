using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ESynchronizationParameter : BaseEntity
    {
        public string Name { get; set; } = null!;
        public DateTime? LastDate { get; set; }
        public decimal? LastId { get; set; }
    }
}
