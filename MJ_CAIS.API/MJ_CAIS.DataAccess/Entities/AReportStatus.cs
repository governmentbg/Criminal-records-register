using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AReportStatus
    {
        public AReportStatus()
        {
            AReportApplications = new HashSet<AReportApplication>();
            AReportStatusHes = new HashSet<AReportStatusH>();
            AReports = new HashSet<AReport>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? StatusType { get; set; }
        public string? Descr { get; set; }

        public virtual ICollection<AReportApplication> AReportApplications { get; set; }
        public virtual ICollection<AReportStatusH> AReportStatusHes { get; set; }
        public virtual ICollection<AReport> AReports { get; set; }
    }
}
