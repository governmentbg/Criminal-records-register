using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUserRole : BaseEntity, IBaseIdEntity
    {
        public string? RoleId { get; set; }
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GRole? Role { get; set; }
        public virtual GUser? User { get; set; }
    }
}
