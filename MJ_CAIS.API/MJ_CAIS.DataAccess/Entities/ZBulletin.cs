using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZBulletin
    {
        public string BulletinId { get; set; } = null!;
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public string? BulletinIndex { get; set; }
        public decimal? BulletinType { get; set; }
        public decimal? BulletinStatus { get; set; }
        public DateTime? BulletinStatusDate { get; set; }
        public string? CourtPrepared { get; set; }
        public string? ActIndex { get; set; }
        public DateTime? ActDate { get; set; }
        public decimal? ActType { get; set; }
        public string? CourtOfAct { get; set; }
        public DateTime? ActExecuteDate { get; set; }
        public string? ExtractInfo { get; set; }
        public string? PersonId { get; set; }
        public string? UserId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? CaseIndex { get; set; }
        public decimal? CaseType { get; set; }
        public DateTime? CaseDate { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? CourtInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public string? IncludedInConviction { get; set; }
        public string? JudgeNotes { get; set; }
        public string? JudgeEdited { get; set; }
        public string? JudgeText { get; set; }
        public DateTime? JudgeEditDate { get; set; }
        public string? R1 { get; set; }
        public string? R2 { get; set; }
    }
}
