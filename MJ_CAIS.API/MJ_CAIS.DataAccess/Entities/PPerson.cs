using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPerson : BaseEntity
    {
        public PPerson()
        {
            DDocuments = new HashSet<DDocument>();
            EEcrisIdentifications = new HashSet<EEcrisIdentification>();
            Fbbcs = new HashSet<Fbbc>();
            PPersGroupFirstPers = new HashSet<PPersGroup>();
            PPersGroupRelPers = new HashSet<PPersGroup>();
        }

        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public decimal? Sex { get; set; }
        public string? Egn { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public decimal? BirthYear { get; set; }
        public decimal? BirthMonth { get; set; }
        public decimal? BirthDay { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? AfisNumber { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthCountryId { get; set; }

        public virtual GCity? BirthCity { get; set; }
        public virtual GCountry? BirthCountry { get; set; }
        public virtual ICollection<DDocument> DDocuments { get; set; }
        public virtual ICollection<EEcrisIdentification> EEcrisIdentifications { get; set; }
        public virtual ICollection<Fbbc> Fbbcs { get; set; }
        public virtual ICollection<PPersGroup> PPersGroupFirstPers { get; set; }
        public virtual ICollection<PPersGroup> PPersGroupRelPers { get; set; }
    }
}
