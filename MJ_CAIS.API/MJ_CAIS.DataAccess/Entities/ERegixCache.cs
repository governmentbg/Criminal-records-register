using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ERegixCache : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? RequestXml { get; set; }
        public string? ResponseXml { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Egn { get; set; }
        public string? WebServiceName { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountryName { get; set; }
        public string? BirthCityName { get; set; }
        public string? BirthMunName { get; set; }
        public string? BirthDistrictName { get; set; }
        public string? Lnch { get; set; }
        public string? BirthCountryCode { get; set; }
        public string? BirthCountryNameLat { get; set; }
        public string? BirthPlace { get; set; }
        public string? Alias { get; set; }
    }
}
