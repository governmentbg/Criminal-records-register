using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonHCitizenship : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? PersonHId { get; set; }
        public string? CountryId { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual PPersonH? PersonH { get; set; }
    }
}
