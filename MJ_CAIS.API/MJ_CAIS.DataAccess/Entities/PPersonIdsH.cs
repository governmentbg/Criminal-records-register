using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonIdsH : BaseEntity, IBaseIdEntity
    {
        public string? Pid { get; set; }
        public string PidTypeId { get; set; } = null!;
        public string? Issuer { get; set; }
        public string? CountryId { get; set; }
        public string? PersonId { get; set; }
        public string Id { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? PersonHId { get; set; }

        public virtual PPersonH? PersonH { get; set; }
    }
}
