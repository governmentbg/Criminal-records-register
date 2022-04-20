using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BEcrisOffLvlPart : BaseEntity, IBaseNomenclature
    {
        public string? EcrisTechnId { get; set; }
        public string? Category { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
    }
}
