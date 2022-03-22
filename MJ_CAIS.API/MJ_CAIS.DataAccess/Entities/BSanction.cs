using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BSanction : BaseEntity
    {
        public string? SanctCategoryId { get; set; }
        public string? Descr { get; set; }
        public string? EcrisSanctCategId { get; set; }
        public decimal? SpecificToMinor { get; set; }
        public DateTime? DecisionStartDate { get; set; }
        public DateTime? DecisionEndDate { get; set; }
        public decimal? DecisionDurationYears { get; set; }
        public DateTime? ExecutionStartDate { get; set; }
        public DateTime? ExecutionEndDate { get; set; }
        public decimal? ExecutionDurationYears { get; set; }
        public string? SanctProbCategId { get; set; }
        public string? SanctProbMeasureId { get; set; }
        public decimal? SanctProbValue { get; set; }
        public decimal? DecisionDurationMonths { get; set; }
        public decimal? DecisionDurationDays { get; set; }
        public decimal? DecisionDurationHours { get; set; }
        public decimal? ExecutionDurationMonths { get; set; }
        public decimal? ExecutionDurationDays { get; set; }
        public decimal? ExecutionDurationHours { get; set; }
        public decimal? FineAmount { get; set; }
        public decimal? SuspentionDurationYears { get; set; }
        public decimal? SuspentionDurationMonths { get; set; }
        public decimal? SuspentionDurationDays { get; set; }
        public decimal? SuspentionDurationHours { get; set; }
        public string? ProbationDescr { get; set; }
        public string? DetenctionDescr { get; set; }
        public string? SanctActivityId { get; set; }
        public string? SanctActivityDescr { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual BEcrisStanctCateg? EcrisSanctCateg { get; set; }
        public virtual BSanctionActivity? SanctActivity { get; set; }
        public virtual BSanctionCategory? SanctCategory { get; set; }
        public virtual BSanctProbCategory? SanctProbCateg { get; set; }
        public virtual BSanctProbMeasure? SanctProbMeasure { get; set; }
    }
}
