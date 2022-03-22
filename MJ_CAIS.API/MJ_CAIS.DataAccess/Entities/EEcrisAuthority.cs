using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisAuthority : BaseEntity, IBaseNomenclature
    {
        public EEcrisAuthority()
        {
            EEcrisMessageFromAuths = new HashSet<EEcrisMessage>();
            EEcrisMessageToAuths = new HashSet<EEcrisMessage>();
        }

        public string? EcrisTechnId { get; set; }
        public string? Iso31662Number { get; set; }
        public string? MemberStateCode { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? CountryId { get; set; }

        public virtual ICollection<EEcrisMessage> EEcrisMessageFromAuths { get; set; }
        public virtual ICollection<EEcrisMessage> EEcrisMessageToAuths { get; set; }
    }
}
