using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BEcrisStanctCateg : BaseEntity, IBaseNomenclature
    {
        public BEcrisStanctCateg()
        {
            BSanctions = new HashSet<BSanction>();
        }

        public string? EcrisTechnId { get; set; }
        public string? Category { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<BSanction> BSanctions { get; set; }
    }
}
