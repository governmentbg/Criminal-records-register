using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MJ_CAIS.IdentityServer.CAISAppCredentials.Entities
{
    public partial class GUsers
    {
        public GUsers()
        {
            GUserRoles = new HashSet<GUserRoles>();
        }

        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Familyname { get; set; }
        public bool? Active { get; set; }
        public string Email { get; set; }
        public string Egn { get; set; }
        public string CsAuthorityId { get; set; }
        public string Position { get; set; }

        public virtual ICollection<GUserRoles> GUserRoles { get; set; }
    }
}
