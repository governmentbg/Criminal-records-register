using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AAppPersAlias : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? ApplicationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? Type { get; set; }

        public virtual AApplication? Application { get; set; }
    }
}
