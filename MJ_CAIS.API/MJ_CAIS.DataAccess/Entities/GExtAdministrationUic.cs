using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GExtAdministrationUic : BaseEntity, IBaseIdEntity
    {
        public GExtAdministrationUic()
        {
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Value { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? AdministrationId { get; set; }

        public virtual GExtAdministration? Administration { get; set; }
    }
}
