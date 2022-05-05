using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PBulletinId : BaseEntity
    {
        public string? PersonId { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual PPersonId? Person { get; set; }
    }
}
