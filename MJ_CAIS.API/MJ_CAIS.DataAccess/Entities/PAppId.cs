using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PAppId : BaseEntity
    {
        public string? ApplicationId { get; set; }
        public string? PersonId { get; set; }

        public virtual AApplication? Application { get; set; }
        public virtual PPersonId? Person { get; set; }
    }
}
