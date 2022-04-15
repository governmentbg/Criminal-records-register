using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AAppPersAlias : BaseEntity
    {
        public string? ApplicationId { get; set; }

        public virtual AApplication? Application { get; set; }
    }
}
