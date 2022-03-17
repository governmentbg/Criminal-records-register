﻿using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMessage : BaseEntity
    {
        public EEcrisMessage()
        {
            DDocuments = new HashSet<DDocument>();
            EEcrisIdentifications = new HashSet<EEcrisIdentification>();
            InverseRequestMsg = new HashSet<EEcrisMessage>();
        }

        public string? RequestMsgId { get; set; }
        public string? FromAuthId { get; set; }
        public string? ToAuthId { get; set; }
        public string? Identifier { get; set; }
        public string? EcrisIdentifier { get; set; }
        public DateTime? MsgTimestamp { get; set; }
        public string? MsgTypeId { get; set; }
        public string? ResponseTypeId { get; set; }
        public string? EcrisMsgStatus { get; set; }
        public string? PersonNames { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountry { get; set; }
        public string? BirthCity { get; set; }
        public string? FbbcId { get; set; }

        public virtual EEcrisMsgStatus? EcrisMsgStatusNavigation { get; set; }
        public virtual EEcrisAuthority? FromAuth { get; set; }
        public virtual EEcrisMsgType? MsgType { get; set; }
        public virtual EEcrisMessage? RequestMsg { get; set; }
        public virtual EEcrisMsgRespType? ResponseType { get; set; }
        public virtual EEcrisAuthority? ToAuth { get; set; }
        public virtual ICollection<DDocument> DDocuments { get; set; }
        public virtual ICollection<EEcrisIdentification> EEcrisIdentifications { get; set; }
        public virtual ICollection<EEcrisMessage> InverseRequestMsg { get; set; }
    }
}
