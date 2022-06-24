using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZPersonNationality
    {
        public string PersonId { get; set; } = null!;
        public decimal Nationality { get; set; }
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public string PersNatlPk { get; set; } = null!;
    }
}
