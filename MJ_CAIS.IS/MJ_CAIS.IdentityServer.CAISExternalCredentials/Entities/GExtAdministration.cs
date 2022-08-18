using System;
using System.Collections.Generic;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities
{
    public partial class GExtAdministration
    {
        public GExtAdministration()
        {
            GExtAdministrationUics = new HashSet<GExtAdministrationUic>();
            GUsersExts = new HashSet<GUsersExt>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string Role { get; set; }

        public virtual ICollection<GExtAdministrationUic> GExtAdministrationUics { get; set; }
        public virtual ICollection<GUsersExt> GUsersExts { get; set; }
    }
}
