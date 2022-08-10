using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WDocument : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public byte[]? Content { get; set; }
        public string? MimeType { get; set; }
        public decimal? Bytes { get; set; }
        public string? Md5Hash { get; set; }
        public string? Sha1Hash { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Descr { get; set; }
        public string? WApplId { get; set; }

        public virtual WApplication? WAppl { get; set; }
    }
}
