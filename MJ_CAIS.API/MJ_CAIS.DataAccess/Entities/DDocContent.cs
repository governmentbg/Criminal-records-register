using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DDocContent
    {
        public DDocContent()
        {
            DDocuments = new HashSet<DDocument>();
        }

        public string Id { get; set; } = null!;
        public byte[]? Content { get; set; }
        public string? MimeType { get; set; }
        public decimal? Bytes { get; set; }
        public string? Md5Hash { get; set; }
        public string? Sha1Hash { get; set; }

        public virtual ICollection<DDocument> DDocuments { get; set; }
    }
}
