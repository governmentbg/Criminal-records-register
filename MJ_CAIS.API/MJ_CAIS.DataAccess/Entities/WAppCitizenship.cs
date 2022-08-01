using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WAppCitizenship : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? CountryId { get; set; }
        public string? WApplicationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual WApplication? WApplication { get; set; }
    }
}
