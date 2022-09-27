using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class NInternalRequest : BaseEntity, IBaseIdEntity
    {
        public NInternalRequest()
        {
            NInternalReqBulletins = new HashSet<NInternalReqBulletin>();
        }

        public string Id { get; set; } = null!;
        public string? RegNumber { get; set; }
        public string? Description { get; set; }
        public string? ReqStatusCode { get; set; }
        public string? ResponseDescr { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string? PPersIdId { get; set; }
        public string? FromAuthorityId { get; set; }
        public string? ToAuthorityId { get; set; }
        public string NIntReqTypeId { get; set; } = null!;
        public DateTime? SentOn { get; set; }
        public string? SentBy { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public string? ProcessedBy { get; set; }

        public virtual GCsAuthority? FromAuthority { get; set; }
        public virtual NIntternalReqType NIntReqType { get; set; } = null!;
        public virtual PPersonId? PPersId { get; set; }
        public virtual GUser? ProcessedByNavigation { get; set; }
        public virtual NReqStatus? ReqStatusCodeNavigation { get; set; }
        public virtual GUser? SentByNavigation { get; set; }
        public virtual GCsAuthority? ToAuthority { get; set; }
        public virtual ICollection<NInternalReqBulletin> NInternalReqBulletins { get; set; }

    }
}
