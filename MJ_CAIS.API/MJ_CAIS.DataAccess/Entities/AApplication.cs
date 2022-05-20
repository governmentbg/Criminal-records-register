using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AApplication : BaseEntity, IBaseIdEntity
    {
        public AApplication()
        {
            AAppCitizenships = new HashSet<AAppCitizenship>();
            AAppPersAliases = new HashSet<AAppPersAlias>();
            ACertificates = new HashSet<ACertificate>();
            AStatusHes = new HashSet<AStatusH>();
            PAppIds = new HashSet<PAppId>();
        }

        public string Id { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public string? Purpose { get; set; }
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
        public string? ApplicantName { get; set; }
        public string? Address { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFullname { get; set; }
        public string? SrvcResRcptMethId { get; set; }
        public string? AddrName { get; set; }
        public string? AddrStr { get; set; }
        public string? AddrDistrict { get; set; }
        public string? AddrTown { get; set; }
        public string? AddrState { get; set; }
        public string? AddrPhone { get; set; }
        public string? AddrEmail { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? ApplicationTypeId { get; set; }
        public string? CsAuthorityId { get; set; }
        public bool? IsLocal { get; set; }
        public string? PurposeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? PaymentMethodId { get; set; }
        public bool? FromCosul { get; set; }
        public string? StatusCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrecision { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? UserCitizenId { get; set; }
        public string? UserId { get; set; }
        public string? UserExtId { get; set; }
        public string? WApplicationId { get; set; }
        public string? FullnameLat { get; set; }
        public string? CsAuthorityBirthId { get; set; }
        public string? Suid { get; set; }
        public string? EgnId { get; set; }
        public string? LnchId { get; set; }
        public string? LnId { get; set; }
        public string? SuidId { get; set; }

        public virtual AApplicationType? ApplicationType { get; set; }
        public virtual GCity? BirthCity { get; set; }
        public virtual GCountry? BirthCountry { get; set; }
        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual GCsAuthority? CsAuthorityBirth { get; set; }
        public virtual PPersonId? EgnNavigation { get; set; }
        public virtual PPersonId? LnNavigation { get; set; }
        public virtual PPersonId? LnchNavigation { get; set; }
        public virtual APaymentMethod? PaymentMethod { get; set; }
        public virtual APurpose? PurposeNavigation { get; set; }
        public virtual ASrvcResRcptMeth? SrvcResRcptMeth { get; set; }
        public virtual PPersonId? SuidNavigation { get; set; }
        public virtual ICollection<AAppCitizenship> AAppCitizenships { get; set; }
        public virtual ICollection<AAppPersAlias> AAppPersAliases { get; set; }
        public virtual ICollection<ACertificate> ACertificates { get; set; }
        public virtual ICollection<AStatusH> AStatusHes { get; set; }
        public virtual ICollection<PAppId> PAppIds { get; set; }
    }
}
