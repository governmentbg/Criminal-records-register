using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BPersNationality : BaseEntity
    {
        public string? CountryId { get; set; }
        public string? BulletinId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual GCountry? Country { get; set; }
    }
}
