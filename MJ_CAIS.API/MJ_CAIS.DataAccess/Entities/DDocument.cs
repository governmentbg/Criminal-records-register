using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DDocument : BaseEntity
    {
        public string? Name { get; set; }
        public string? DocTypeId { get; set; }
        public string? PersonId { get; set; }
        public string? EcrisMsgId { get; set; }
        public string? IsinMsgId { get; set; }
        public string? EissMsgId { get; set; }
        public string? FbbcId { get; set; }
        public string? ApplicationId { get; set; }
        public string? Descr { get; set; }
        public string? EisppId { get; set; }
        public string? DocContentId { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual DDocContent? DocContent { get; set; }
        public virtual DDocType? DocType { get; set; }
        public virtual EEcrisMessage? EcrisMsg { get; set; }
        public virtual Fbbc? Fbbc { get; set; }
        public virtual PPerson? Person { get; set; }
    }
}
