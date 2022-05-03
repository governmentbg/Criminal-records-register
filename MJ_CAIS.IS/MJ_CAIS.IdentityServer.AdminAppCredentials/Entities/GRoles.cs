using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MJ_CAIS.IdentityServer.CAISAppCredentials.Entities
{
    public partial class GRoles
    {
        public GRoles()
        {
            GUserRoles = new HashSet<GUserRoles>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GUserRoles> GUserRoles { get; set; }
    }
}
