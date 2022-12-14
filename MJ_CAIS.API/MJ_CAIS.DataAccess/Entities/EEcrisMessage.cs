using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMessage : BaseEntity, IBaseIdEntity
    {
        public EEcrisMessage()
        {
            DDocuments = new HashSet<DDocument>();
            EEcrisIdentifications = new HashSet<EEcrisIdentification>();
            EEcrisInboxes = new HashSet<EEcrisInbox>();
            EEcrisMsgNames = new HashSet<EEcrisMsgName>();
            EEcrisMsgNationalities = new HashSet<EEcrisMsgNationality>();
            EEcrisOutboxes = new HashSet<EEcrisOutbox>();
            EEcrisReferences = new HashSet<EEcrisReference>();
            EWebRequests = new HashSet<EWebRequest>();
            InverseRequestMsg = new HashSet<EEcrisMessage>();
        }

        public string Id { get; set; } = null!;
        public string? RequestMsgId { get; set; }
        public string? FromAuthId { get; set; }
        public string? ToAuthId { get; set; }
        public string? Identifier { get; set; }
        public string? EcrisIdentifier { get; set; }
        public DateTime? MsgTimestamp { get; set; }
        public string? ResponseTypeId { get; set; }
        public string? EcrisMsgStatus { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountry { get; set; }
        public string? BirthCity { get; set; }
        public string? FbbcId { get; set; }
        public decimal? Sex { get; set; }
        public string? MsgTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? EcrisMsgConvictionId { get; set; }
        public string? Pin { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? Urgent { get; set; }
        public string? BulletinId { get; set; }

        public virtual EEcrisMsgStatus? EcrisMsgStatusNavigation { get; set; }
        public virtual EEcrisAuthority? FromAuth { get; set; }
        public virtual EEcrisMessage? RequestMsg { get; set; }
        public virtual EEcrisAuthority? ToAuth { get; set; }
        public virtual ICollection<DDocument> DDocuments { get; set; }
        public virtual ICollection<EEcrisIdentification> EEcrisIdentifications { get; set; }
        public virtual ICollection<EEcrisInbox> EEcrisInboxes { get; set; }
        public virtual ICollection<EEcrisMsgName> EEcrisMsgNames { get; set; }
        public virtual ICollection<EEcrisMsgNationality> EEcrisMsgNationalities { get; set; }
        public virtual ICollection<EEcrisOutbox> EEcrisOutboxes { get; set; }
        public virtual ICollection<EEcrisReference> EEcrisReferences { get; set; }
        public virtual ICollection<EWebRequest> EWebRequests { get; set; }
        public virtual ICollection<EEcrisMessage> InverseRequestMsg { get; set; }
    }
}
