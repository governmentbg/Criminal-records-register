using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BIdDocCategory : BaseEntity, IBaseNomenclature
    {
        public BIdDocCategory()
        {
            BBulletins = new HashSet<BBulletin>();
        }

        public string? EcrisTechnId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
    }
}
