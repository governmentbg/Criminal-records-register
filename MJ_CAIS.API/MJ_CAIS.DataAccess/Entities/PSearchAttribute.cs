using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PSearchAttribute
    {
        public string PidTypeId { get; set; } = null!;
        public string Pid { get; set; } = null!;
        public string PidIssuer { get; set; } = null!;
        public string CountryId { get; set; } = null!;
        public DateTime? PsaBirthDate { get; set; }
        public bool PsaBgCitizen { get; set; }
        public bool PsaNonbgCitizen { get; set; }
        public string? PsaFirstnameBg { get; set; }
        public string? PsaSurnameBg { get; set; }
        public string? PsaFamilynameBg { get; set; }
        public string? PsaFullnameBg { get; set; }
        public string? PsaInitialsBg { get; set; }
        public string? PsaTwoWordsOfNameBg { get; set; }
        public string? PsaTwoInitialsOfNameBg { get; set; }

        public virtual GCountry Country { get; set; } = null!;
        public virtual PPersonId PPersonId { get; set; } = null!;
        public virtual PPersonIdType PidType { get; set; } = null!;
    }
}
