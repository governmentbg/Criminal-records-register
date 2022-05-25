using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BDecisionChType : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public BDecisionChType()
        {
            BDecisions = new HashSet<BDecision>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Category { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public decimal? OrderNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<BDecision> BDecisions { get; set; }
    }
}
