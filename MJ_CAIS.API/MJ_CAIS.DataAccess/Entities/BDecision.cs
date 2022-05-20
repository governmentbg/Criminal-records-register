using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BDecision : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? DecisionChTypeId { get; set; }
        public string? DecisionEcli { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionAuthId { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? BulletinId { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Descr { get; set; }
        public decimal? Version { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual GDecidingAuthority? DecisionAuth { get; set; }
        public virtual BDecisionChType? DecisionChType { get; set; }
        public virtual BDecisionType? DecisionType { get; set; }
    }
}
