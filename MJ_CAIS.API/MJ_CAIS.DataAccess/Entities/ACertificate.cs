using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ACertificate : BaseEntity, IBaseIdEntity
    {
        public ACertificate()
        {
            AAppBulletins = new HashSet<AAppBulletin>();
            AStatusHes = new HashSet<AStatusH>();
            EEdeliveryMsgs = new HashSet<EEdeliveryMsg>();
        }

        public string Id { get; set; } = null!;
        public string? ApplicationId { get; set; }
        public string? StatusCode { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public string? AccessCode1 { get; set; }
        public string? AccessCode2 { get; set; }
        public string? DocId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual AApplication? Application { get; set; }
        public virtual DDocument? Doc { get; set; }
        public virtual GUser? FirstSigner { get; set; }
        public virtual GUser? SecondSigner { get; set; }
        public virtual AApplicationStatus? StatusCodeNavigation { get; set; }
        public virtual WCertificate WCertificate { get; set; } = null!;
        public virtual ICollection<AAppBulletin> AAppBulletins { get; set; }
        public virtual ICollection<AStatusH> AStatusHes { get; set; }
        public virtual ICollection<EEdeliveryMsg> EEdeliveryMsgs { get; set; }
    }
}
