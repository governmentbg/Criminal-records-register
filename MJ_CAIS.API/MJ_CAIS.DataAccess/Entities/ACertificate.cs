using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ACertificate : BaseEntity
    {
        public ACertificate()
        {
            AAppBulletins = new HashSet<AAppBulletin>();
        }

        public string? ApplicationId { get; set; }
        public string? StatusCode { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public string? AccessCode1 { get; set; }
        public string? AccessCode2 { get; set; }

        public virtual AApplication? Application { get; set; }
        public virtual GUser? FirstSigner { get; set; }
        public virtual GUser? SecondSigner { get; set; }
        public virtual AApplicationStatus? StatusCodeNavigation { get; set; }
        public virtual ICollection<AAppBulletin> AAppBulletins { get; set; }
    }
}
