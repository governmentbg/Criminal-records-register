using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EIsinMsg : BaseEntity
    {
        public EIsinMsg()
        {
            EIsinData = new HashSet<EIsinDatum>();
        }

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

        public virtual ICollection<EIsinDatum> EIsinData { get; set; }
    }
}
