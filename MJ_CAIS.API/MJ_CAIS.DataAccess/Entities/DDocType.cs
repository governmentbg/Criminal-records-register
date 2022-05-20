using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DDocType : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public DDocType()
        {
            DDocuments = new HashSet<DDocument>();
        }
        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Xslt { get; set; }
        public decimal? Visible { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public string? SystemCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<DDocument> DDocuments { get; set; }
    }
}
