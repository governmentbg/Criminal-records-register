using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EWebRequest : BaseEntity, IBaseIdEntity
    {
        public EWebRequest()
        {
            EFieldsRequests = new HashSet<EFieldsRequest>();
            EIsinData = new HashSet<EIsinDatum>();
        }

        public string Id { get; set; } = null!;
        public string? RequestXml { get; set; }
        public string? ResponseXml { get; set; }
        public string? RemoteAddress { get; set; }
        public string? Status { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
        public decimal? Attempts { get; set; }
        public bool? IsFromCache { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CallContext { get; set; }
        public string? ApiServiceCallId { get; set; }
        public string? BulletinId { get; set; }
        public string? ApplicationId { get; set; }
        public string? EcrisMsgId { get; set; }
        public string? WebServiceId { get; set; }
        public bool? IsAsync { get; set; }
        public string? WApplicationId { get; set; }
        public string? ARepApplId { get; set; }

        public virtual AReportApplication? ARepAppl { get; set; }
        public virtual AApplication? Application { get; set; }
        public virtual BBulletin? Bulletin { get; set; }
        public virtual EEcrisMessage? EcrisMsg { get; set; }
        public virtual WApplication? WApplication { get; set; }
        public virtual EWebService? WebService { get; set; }
        public virtual ICollection<EFieldsRequest> EFieldsRequests { get; set; }
        public virtual ICollection<EIsinDatum> EIsinData { get; set; }
    }
}
