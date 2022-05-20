using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersGroup : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? FirstPersId { get; set; }
        public string? RelPersId { get; set; }

        public virtual PPerson? FirstPers { get; set; }
        public virtual PPerson? RelPers { get; set; }
    }
}
