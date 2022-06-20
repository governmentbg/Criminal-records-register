using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BPersNationality : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string CountryId { get; set; } = null!;
        public string BulletinId { get; set; } = null!;
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual BBulletin Bulletin { get; set; } = null!;
        public virtual GCountry Country { get; set; } = null!;
    }
}
