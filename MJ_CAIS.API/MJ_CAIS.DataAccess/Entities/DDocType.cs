using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DDocType : BaseEntity, IBaseNomenclature
    {
        public DDocType()
        {
            DDocuments = new HashSet<DDocument>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Xslt { get; set; }
        public decimal? Visible { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public string? SystemCode { get; set; }

        public virtual ICollection<DDocument> DDocuments { get; set; }
    }
}