using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BCaseType
    {
        public BCaseType()
        {
            BBulletins = new HashSet<BBulletin>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ExternalId { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
    }
}
