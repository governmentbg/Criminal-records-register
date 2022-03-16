using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BPersNationality : BaseEntity
    {
        public string? CountryId { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual GCountry? Country { get; set; }
    }
}
