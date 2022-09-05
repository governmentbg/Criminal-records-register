using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZUser
    {
        public ZUser()
        {
            ZUserRoles = new HashSet<ZUserRole>();
        }

        public string UserId { get; set; } = null!;
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public string? FaxUser { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
        public DateTime? LockDate { get; set; }
        public byte Status { get; set; }
        public string? NamesUser { get; set; }
        public decimal? CityCode { get; set; }
        public string? PhonesUser { get; set; }
        public string? EmailUser { get; set; }
        public string? Employment { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<ZUserRole> ZUserRoles { get; set; }
    }
}
