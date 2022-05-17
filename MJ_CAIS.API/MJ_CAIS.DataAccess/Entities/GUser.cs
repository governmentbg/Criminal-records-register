using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUser : BaseEntity, ICsAuthorityIdFilter
    {
        public GUser()
        {
            ACertificateFirstSigners = new HashSet<ACertificate>();
            ACertificateSecondSigners = new HashSet<ACertificate>();
            GUserRoles = new HashSet<GUserRole>();
        }

        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public bool? Active { get; set; }
        public string? Email { get; set; }
        public string? Egn { get; set; }
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
        public virtual ICollection<GUserRole> GUserRoles { get; set; }
    }
}
