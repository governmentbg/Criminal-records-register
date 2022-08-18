using System;
using System.Collections.Generic;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities
{
    public partial class GExtAdministrationUic
    {
        public string Id { get; set; }
        public string ExtAdmId { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual GExtAdministration ExtAdm { get; set; }
    }
}
