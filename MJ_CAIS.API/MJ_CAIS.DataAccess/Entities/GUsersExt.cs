using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUsersExt : BaseEntity, IBaseIdEntity
    {
        public GUsersExt()
        {
            WApplications = new HashSet<WApplication>();
        }

        public string Id { get; set; } = null!;
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string? AdministrationId { get; set; }
        public bool? Active { get; set; }
        public string? Position { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? RegCertSubject { get; set; }
        public string? UserName { get; set; }

        public virtual GExtAdministration? Administration { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
