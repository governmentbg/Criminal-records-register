using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EIsinMsg : BaseEntity
    {
        public DateTime? MsgDatetime { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? HasError { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public string? RequestContent { get; set; }
        public string? ResponseContent { get; set; }
        public string? Identifier { get; set; }
        public string? ApplicationId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
