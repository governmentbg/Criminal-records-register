using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GDecidingAuthoritiesTmp : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public decimal? EisppId { get; set; }
        public string? EisppCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? ActiveForBulletins { get; set; }
        public decimal? OrderNumber { get; set; }
        public string? ParentId { get; set; }
        public bool? Visible { get; set; }
        public bool? IsGroup { get; set; }
        public string? DisplayName { get; set; }
        public string? OldId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
