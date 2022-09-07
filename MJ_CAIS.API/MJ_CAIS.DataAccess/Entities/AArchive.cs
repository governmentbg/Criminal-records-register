using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AArchive : BaseEntity, IBaseIdEntity
    {
        public AArchive()
        {
            AArchiveDocuments = new HashSet<AArchiveDocument>();
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
        public string? FullnameLat { get; set; }
        public string? Aliases { get; set; }
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
        public string? SrvcResRcptMethName { get; set; }
        public string? AddrName { get; set; }
        public string? AddrStr { get; set; }
        public string? AddrDistrict { get; set; }
        public string? AddrTown { get; set; }
        public string? AddrState { get; set; }
        public string? AddrPhone { get; set; }
        public string? AddrEmail { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string ApplicationTypeId { get; set; } = null!;
        public string? ApplicationTypeName { get; set; }
        public string CsAuthorityId { get; set; } = null!;
        public string? CsAuthorityName { get; set; }
        public string? PurposeId { get; set; }
        public string? PurposeName { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }
        public bool? FromCosul { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrecision { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCountryName { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthCityEkatte { get; set; }
        public string? BirthCityName { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? UserCitizenId { get; set; }
        public string? UserCitizenName { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserExtId { get; set; }
        public string? UserExtName { get; set; }
        public string? WApplicationId { get; set; }
        public string? Suid { get; set; }
        public string? ServiceMigrationId { get; set; }
        public string? PersonIdCsc { get; set; }
        public string? Nationality1Id { get; set; }
        public string? Nationality2Id { get; set; }
        public string? Nationality3Id { get; set; }
        public string? Nationality4Id { get; set; }
        public string? Nationality1Name { get; set; }
        public string? Nationality2Name { get; set; }
        public string? Nationality3Name { get; set; }
        public string? Nationality4Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedByNames { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedByNames { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? PayApprDate { get; set; }
        public string? PayApprUser { get; set; }
        public DateTime? FreepayApprDate { get; set; }
        public string? FreepayApprUser { get; set; }
        public DateTime? BulletinsCheckDate { get; set; }
        public string? BulletinsCheckUsr { get; set; }
        public DateTime? BulletinsSelectDate { get; set; }
        public string? BulletinsSelectUsr { get; set; }
        public DateTime? PrintedDate { get; set; }
        public string? PrintedByUsr { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryUsr { get; set; }
        public DateTime? CancelationDate { get; set; }
        public string? CancelationUsr { get; set; }
        public string? CancelationDescr { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public string? FirstSignerNames { get; set; }
        public string? SecondSignerNames { get; set; }

        public virtual ICollection<AArchiveDocument> AArchiveDocuments { get; set; }
    }
}
