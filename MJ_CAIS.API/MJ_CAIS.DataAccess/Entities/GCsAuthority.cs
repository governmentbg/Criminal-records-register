using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCsAuthority : IBaseNomenclature
    {
        public GCsAuthority()
        {
            BBulletins = new HashSet<BBulletin>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? DecidingAuthId { get; set; }
        public decimal? IsCentral { get; set; }
        public string? OldId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual GDecidingAuthority? DecidingAuth { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
    }
}
