using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZPerson
    {
        public string PersonId { get; set; } = null!;
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? GivenName { get; set; }
        public string? Surname { get; set; }
        public string? FamilyName { get; set; }
        public string? ForeignerNames { get; set; }
        public byte? Sex { get; set; }
        public string? MothersNames { get; set; }
        public string? FathersNames { get; set; }
        public DateTime? Birthdate { get; set; }
        public decimal? BirthplaceCode { get; set; }
        public string? BirthplaceTextForeigner { get; set; }
        public decimal? PresentCity { get; set; }
        public string? PresentAddress { get; set; }
        public string? PersonIdIdent { get; set; }
        public decimal? PersonIdFirst { get; set; }
        public byte? Status { get; set; }
        public byte? ErrorStatus { get; set; }
        public string? CourtInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public string? TransformationStatus { get; set; }
        public string? R1 { get; set; }
        public string? R2 { get; set; }
    }
}
