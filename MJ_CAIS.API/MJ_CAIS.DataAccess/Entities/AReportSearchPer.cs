using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AReportSearchPer : BaseEntity, IBaseIdEntity
    {
        public AReportSearchPer()
        {
            ARepPers = new HashSet<ARepPer>();
        }

        public string Id { get; set; } = null!;
        public string? CServiceUri { get; set; }
        public string? CServiceType { get; set; }
        public string? CEmplId { get; set; }
        public string? CEmpNames { get; set; }
        public string? CEmpAddId { get; set; }
        public string? CEmpPos { get; set; }
        public string? CRespPersId { get; set; }
        public string? CLawReason { get; set; }
        public string? CRemark { get; set; }
        public string? CAdministrationOid { get; set; }
        public string? CAdministrationName { get; set; }
        public string? Fullname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? BirthdatePrec { get; set; }
        public string? Birthplace { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public decimal? ApiServiceCallId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<ARepPer> ARepPers { get; set; }
    }
}
