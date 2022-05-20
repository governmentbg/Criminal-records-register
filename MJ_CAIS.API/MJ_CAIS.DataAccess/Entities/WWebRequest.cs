using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WWebRequest : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? ApplicationId { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CallContext { get; set; }
        public string? ApiServiceCallId { get; set; }
        public decimal? Version { get; set; }
        public string? WebRequestId { get; set; }
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
    }
}
