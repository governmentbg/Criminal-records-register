using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GExtAdministration : BaseEntity, IBaseIdEntity
    {
        public GExtAdministration()
        {
            GUsersExts = new HashSet<GUsersExt>();
            GExtAdministrationUics = new HashSet<GExtAdministrationUic>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<GUsersExt> GUsersExts { get; set; }
        public virtual ICollection<GExtAdministrationUic> GExtAdministrationUics { get; set; }
    }
}
