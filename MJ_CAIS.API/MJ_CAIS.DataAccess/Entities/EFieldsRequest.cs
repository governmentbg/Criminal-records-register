using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EFieldsRequest : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? AApplId { get; set; }
        public string EWebReqId { get; set; } = null!;
        public string? WApplId { get; set; }
        public string? ARepApplId { get; set; }
        public string FieldsDescription { get; set; } = null!;

        public virtual AApplication? AAppl { get; set; }
        public virtual AReportApplication? ARepAppl { get; set; }
        public virtual EWebRequest EWebReq { get; set; } = null!;
        public virtual WApplication? WAppl { get; set; }
    }
}
