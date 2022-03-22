using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BDecisionType : BaseEntity, IBaseNomenclature
    {
        public BDecisionType()
        {
            BBulletins = new HashSet<BBulletin>();
            BDecisions = new HashSet<BDecision>();
        }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ExternalId { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BDecision> BDecisions { get; set; }
    }
}
