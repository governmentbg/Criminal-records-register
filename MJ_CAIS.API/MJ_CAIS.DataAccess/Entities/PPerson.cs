using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPerson : BaseEntity
    {
        public PPerson()
        {
            AApplications = new HashSet<AApplication>();
            DDocuments = new HashSet<DDocument>();
            EEcrisIdentifications = new HashSet<EEcrisIdentification>();
            Fbbcs = new HashSet<Fbbc>();
            PPersGroupFirstPers = new HashSet<PPersGroup>();
            PPersGroupRelPers = new HashSet<PPersGroup>();
            PPersonAliases = new HashSet<PPersonAlias>();
        }

        public string? PersGroupId { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<DDocument> DDocuments { get; set; }
        public virtual ICollection<EEcrisIdentification> EEcrisIdentifications { get; set; }
        public virtual ICollection<Fbbc> Fbbcs { get; set; }
        public virtual ICollection<PPersGroup> PPersGroupFirstPers { get; set; }
        public virtual ICollection<PPersGroup> PPersGroupRelPers { get; set; }
        public virtual ICollection<PPersonAlias> PPersonAliases { get; set; }
    }
}
