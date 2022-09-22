using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.ExtUsers
{
    public partial class LocalGUsersExt : IdentityUser<string>
    {
        public string Egn { get; set; }
        public string Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string AdministrationId { get; set; }
        public bool? Active { get; set; }
        public string Position { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string? RegCertSubject { get; set; }
        public string? Remarks { get; set; }
        public bool? Denied { get; set; }
    }
}
