using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BDecision : BaseEntity
    {
        public string? DecisionChTypeId { get; set; }
        public string? DecisionEcli { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionAuthId { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? Descr { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual GDecidingAuthority? DecisionAuth { get; set; }
        public virtual BDecisionChType? DecisionChType { get; set; }
        public virtual BDecisionType? DecisionType { get; set; }
    }
}
