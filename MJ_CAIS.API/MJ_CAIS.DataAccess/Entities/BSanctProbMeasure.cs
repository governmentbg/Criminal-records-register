using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BSanctProbMeasure : BaseEntity, IBaseNomenclature
    {
        public BSanctProbMeasure()
        {
            BProbations = new HashSet<BProbation>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ExternalId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<BProbation> BProbations { get; set; }
    }
}
