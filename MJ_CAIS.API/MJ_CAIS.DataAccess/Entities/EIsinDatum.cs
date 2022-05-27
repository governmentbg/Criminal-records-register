using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EIsinDatum : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? IdentifierType { get; set; }
        public string? Identifier { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Sex { get; set; }
        public string? Country1Code { get; set; }
        public string? Country1Name { get; set; }
        public string? Country2Code { get; set; }
        public string? Country2Name { get; set; }
        public string? BirthcountryCode { get; set; }
        public string? BirthcountryName { get; set; }
        public string? BirthPlace { get; set; }
        public DateTime? SanctionStartDate { get; set; }
        public DateTime? SanctionEndDate { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionAuthCode { get; set; }
        public string? DecisionAuthName { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? CaseTypeId { get; set; }
        public string? CaseAuthCode { get; set; }
        public string? CaseAuthName { get; set; }
        public string? BulletinId { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? WebRequestId { get; set; }
        public string? SourceType { get; set; }
        public string? SanctionType { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual EWebRequest? WebRequest { get; set; }
    }
}
