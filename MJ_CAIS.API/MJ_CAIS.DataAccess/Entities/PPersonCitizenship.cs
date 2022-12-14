using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonCitizenship : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string PersonId { get; set; } = null!;
        public string CountryId { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GCountry Country { get; set; } = null!;
        public virtual PPerson Person { get; set; } = null!;
    }
}
