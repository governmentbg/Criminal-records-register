using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MJ_CAIS.IdentityServer.CAISAppCredentials.Entities
{
    public partial class GUserRoles
    {
        public string RoleId { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }

        public virtual GRoles Role { get; set; }
        public virtual GUsers User { get; set; }
    }
}
