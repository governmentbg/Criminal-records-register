using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities
{
    public partial class GUsersExt
    {
        public string Id { get; set; }
        public string Egn { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string AdministrationId { get; set; }
        public bool? Active { get; set; }
        public string Position { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public virtual GExtAdministration? Administration { get; set; }
    }
}
