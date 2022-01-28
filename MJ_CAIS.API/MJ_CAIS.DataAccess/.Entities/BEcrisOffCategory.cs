﻿using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BEcrisOffCategory
    {
        public BEcrisOffCategory()
        {
            BOffences = new HashSet<BOffence>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Category { get; set; }
        public decimal? CategoryIsOpen { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual ICollection<BOffence> BOffences { get; set; }
    }
}