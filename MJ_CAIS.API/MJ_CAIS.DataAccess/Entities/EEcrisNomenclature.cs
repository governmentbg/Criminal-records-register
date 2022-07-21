using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisNomenclature : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? NomCode { get; set; }
        public string? EcrisTechId { get; set; }
        public string? Num { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? NameBg { get; set; }
        public string? NameEn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
