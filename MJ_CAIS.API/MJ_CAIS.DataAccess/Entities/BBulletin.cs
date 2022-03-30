using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BBulletin : BaseEntity
    {
        public BBulletin()
        {
            BBullPersAliases = new HashSet<BBullPersAlias>();
            BDecisions = new HashSet<BDecision>();
            BInternalRequests = new HashSet<BInternalRequest>();
            BOffences = new HashSet<BOffence>();
            BPersNationalities = new HashSet<BPersNationality>();
            BSanctions = new HashSet<BSanction>();
            DDocuments = new HashSet<DDocument>();
            EIsinData = new HashSet<EIsinDatum>();
        }

        public decimal? Version { get; set; }
        public string? CsAuthorityId { get; set; }
        public string? RegistrationNumber { get; set; }
        public decimal? SequentialIndex { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecidingAuthId { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? CaseTypeId { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? ConvRemarks { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string? DecisionEcli { get; set; }
        public DateTime? BulletinCreateDate { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? BulletinAuthorityId { get; set; }
        public string? CreatedByNames { get; set; }
        public string? ApprovedByNames { get; set; }
        public string? ApprovedByPosition { get; set; }
        public string? StatusId { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public decimal Sex { get; set; }
        public string? Egn { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrecision { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? FullnameLat { get; set; }
        public string? IdDocNumber { get; set; }
        public string? IdDocCategoryId { get; set; }
        public string? IdDocTypeDescr { get; set; }
        public string? IdDocIssuingAuthority { get; set; }
        public DateTime? IdDocIssuingDate { get; set; }
        public string? IdDocIssuingDatePrec { get; set; }
        public DateTime? IdDocValidDate { get; set; }
        public string? IdDocValidDatePrec { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? FatherFullname { get; set; }
        public string? MotherSurname { get; set; }
        public string? AfisNumber { get; set; }
        public decimal? ConvIsTransmittable { get; set; }
        public DateTime? ConvRetPeriodEndDate { get; set; }
        public string? CreatedByPosition { get; set; }
        public string? BulletinType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? RehabilitationDate { get; set; }
        public string? EcrisConvictionId { get; set; }

        public virtual GCity? BirthCity { get; set; }
        public virtual GCountry? BirthCountry { get; set; }
        public virtual GDecidingAuthority? BulletinAuthority { get; set; }
        public virtual BCaseType? CaseType { get; set; }
        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual GDecidingAuthority? DecidingAuth { get; set; }
        public virtual BDecisionType? DecisionType { get; set; }
        public virtual BIdDocCategory? IdDocCategory { get; set; }
        public virtual BBulletinStatus? Status { get; set; }
        public virtual ICollection<BBullPersAlias> BBullPersAliases { get; set; }
        public virtual ICollection<BDecision> BDecisions { get; set; }
        public virtual ICollection<BInternalRequest> BInternalRequests { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<BPersNationality> BPersNationalities { get; set; }
        public virtual ICollection<BSanction> BSanctions { get; set; }
        public virtual ICollection<DDocument> DDocuments { get; set; }
        public virtual ICollection<EIsinDatum> EIsinData { get; set; }
    }
}
