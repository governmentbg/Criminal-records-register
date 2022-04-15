using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AAppCitizenship : BaseEntity
    {
        public string? CountryId { get; set; }
        public string? ApplicationId { get; set; }

        public virtual AApplication? Application { get; set; }
        public virtual GCountry? Country { get; set; }
    }
}
