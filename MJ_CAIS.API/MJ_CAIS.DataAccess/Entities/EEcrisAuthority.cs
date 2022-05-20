using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisAuthority : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public EEcrisAuthority()
        {
            EEcrisMessageFromAuths = new HashSet<EEcrisMessage>();
            EEcrisMessageToAuths = new HashSet<EEcrisMessage>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Iso31662Number { get; set; }
        public string? MemberStateCode { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? CountryId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<EEcrisMessage> EEcrisMessageFromAuths { get; set; }
        public virtual ICollection<EEcrisMessage> EEcrisMessageToAuths { get; set; }
    }
}
