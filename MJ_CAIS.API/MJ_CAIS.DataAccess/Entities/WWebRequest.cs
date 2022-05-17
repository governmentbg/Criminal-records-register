using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WWebRequest : BaseEntity
    {
        public string? ApplicationId { get; set; }

        public virtual WApplication? Application { get; set; }
    }
}
