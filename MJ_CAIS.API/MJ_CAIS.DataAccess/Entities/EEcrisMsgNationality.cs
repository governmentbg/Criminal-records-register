using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMsgNationality : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? EEcrisMsgId { get; set; }
        public string? CountryId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual EEcrisMessage? EEcrisMsg { get; set; }
    }
}
