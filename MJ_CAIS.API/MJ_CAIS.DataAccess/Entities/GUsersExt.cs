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
        public bool? EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? UserName { get; set; }
        public string? Remarks { get; set; }
        public bool? Denied { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? NormalizedEmail { get; set; }

        public virtual GExtAdministration? Administration { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
