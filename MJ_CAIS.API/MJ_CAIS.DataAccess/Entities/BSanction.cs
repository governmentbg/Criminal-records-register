using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BSanction : BaseEntity
    {
        public BSanction()
        {
            BProbations = new HashSet<BProbation>();
        }

        public string? SanctCategoryId { get; set; }
        public string? Descr { get; set; }
        public string? EcrisSanctCategId { get; set; }
        public byte? DecisionDurationYears { get; set; }
        public byte? DecisionDurationMonths { get; set; }
        public byte? DecisionDurationDays { get; set; }
        public byte? DecisionDurationHours { get; set; }
        public decimal? FineAmount { get; set; }
        public byte? SuspentionDurationYears { get; set; }
        public byte? SuspentionDurationMonths { get; set; }
        public byte? SuspentionDurationDays { get; set; }
        public byte? SuspentionDurationHours { get; set; }
        public string? DetenctionDescr { get; set; }
        public string? BulletinId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual BEcrisStanctCateg? EcrisSanctCateg { get; set; }
        public virtual BSanctionCategory? SanctCategory { get; set; }
        public virtual ICollection<BProbation> BProbations { get; set; }
    }
}
