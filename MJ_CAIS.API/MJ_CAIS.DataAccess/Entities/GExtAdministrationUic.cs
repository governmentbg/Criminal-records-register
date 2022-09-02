using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GExtAdministrationUic : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string ExtAdmId { get; set; } = null!;
        public string Value { get; set; } = null!;
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Name { get; set; }

        public virtual GExtAdministration ExtAdm { get; set; } = null!;
    }
}
