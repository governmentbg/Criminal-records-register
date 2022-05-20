using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BProbation : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? SanctProbCategId { get; set; }
        public string? SanctProbMeasureId { get; set; }
        public decimal? SanctProbValue { get; set; }
        public byte? DecisionDurationYears { get; set; }
        public byte? DecisionDurationMonths { get; set; }
        public byte? DecisionDurationDays { get; set; }
        public byte? DecisionDurationHours { get; set; }
        public string? SanctionId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual BSanctProbCategory? SanctProbCateg { get; set; }
        public virtual BSanctProbMeasure? SanctProbMeasure { get; set; }
        public virtual BSanction? Sanction { get; set; }
    }
}
