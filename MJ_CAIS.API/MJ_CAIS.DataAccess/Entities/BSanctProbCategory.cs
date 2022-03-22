using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BSanctProbCategory : BaseEntity, IBaseNomenclature
    {
        public BSanctProbCategory()
        {
            BSanctions = new HashSet<BSanction>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ExternalId { get; set; }

        public virtual ICollection<BSanction> BSanctions { get; set; }
    }
}
