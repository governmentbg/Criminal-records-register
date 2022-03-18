using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GNomenclature : BaseEntity
    {
        public string? TableName { get; set; }
        public string? Descr { get; set; }
    }
}
