using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonAlias : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? Type { get; set; }
        public string? PersonId { get; set; }
        public decimal? Sex { get; set; }
        public string? Egn { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrec { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCityId { get; set; }

        public virtual GCity? BirthCity { get; set; }
        public virtual GCountry? BirthCountry { get; set; }
        public virtual PPerson? Person { get; set; }
    }
}
