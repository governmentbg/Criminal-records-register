using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUser : BaseEntity
    {
        public GUser()
        {
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

        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual ICollection<GUserRole> GUserRoles { get; set; }
    }
}
