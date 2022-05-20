using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BDecisionType : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public BDecisionType()
        {
            BBulletins = new HashSet<BBulletin>();
            BDecisions = new HashSet<BDecision>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ExternalId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BDecision> BDecisions { get; set; }
    }
}
