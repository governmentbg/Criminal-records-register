using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ESynchronizationParameter : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime? LastDate { get; set; }
        public decimal? LastId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
