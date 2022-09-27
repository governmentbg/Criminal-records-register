using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUser : BaseEntity, IBaseIdEntity
    {
        public GUser()
        {
            ACertificateFirstSigners = new HashSet<ACertificate>();
            ACertificateSecondSigners = new HashSet<ACertificate>();
            AReportFirstSigners = new HashSet<AReport>();
            AReportSecondSigners = new HashSet<AReport>();
            GUserRoles = new HashSet<GUserRole>();
            NInternalRequestProcessedByNavigations = new HashSet<NInternalRequest>();
            NInternalRequestSentByNavigations = new HashSet<NInternalRequest>();
        }

        public string Id { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public bool? Active { get; set; }
        public string? Email { get; set; }
        public string Egn { get; set; } = null!;
        public string? CsAuthorityId { get; set; }
        public string? Position { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual ICollection<ACertificate> ACertificateFirstSigners { get; set; }
        public virtual ICollection<ACertificate> ACertificateSecondSigners { get; set; }
        public virtual ICollection<AReport> AReportFirstSigners { get; set; }
        public virtual ICollection<AReport> AReportSecondSigners { get; set; }
        public virtual ICollection<GUserRole> GUserRoles { get; set; }
        public virtual ICollection<NInternalRequest> NInternalRequestProcessedByNavigations { get; set; }
        public virtual ICollection<NInternalRequest> NInternalRequestSentByNavigations { get; set; }
    }
}
