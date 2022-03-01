﻿using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCountry : IBaseNomenclature
    {
        public GCountry()
        {
            BBulletins = new HashSet<BBulletin>();
            BOffences = new HashSet<BOffence>();
            BPersNationalities = new HashSet<BPersNationality>();
            GCities = new HashSet<GCity>();
            GCountrySubdivisions = new HashSet<GCountrySubdivision>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Iso31662Number { get; set; }
        public string? Iso31662Code { get; set; }
        public decimal? UsedForNationality { get; set; }
        public string? Remark { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<BPersNationality> BPersNationalities { get; set; }
        public virtual ICollection<GCity> GCities { get; set; }
        public virtual ICollection<GCountrySubdivision> GCountrySubdivisions { get; set; }
    }
}
