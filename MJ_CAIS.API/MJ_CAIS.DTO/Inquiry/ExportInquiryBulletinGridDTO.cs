﻿namespace MJ_CAIS.DTO.Inquiry
{
    public class ExportInquiryBulletinGridDTO : InquiryBulletinGridDTO
    {
        public decimal? SequentialIndex { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string? DecisionEcli { get; set; }
        public DateTime? BulletinCreateDate { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? CreatedByNames { get; set; }
        public string? ApprovedByNames { get; set; }
        public string? ApprovedByPosition { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public decimal Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? FullnameLat { get; set; }
        public string? IdDocNumber { get; set; }
        public string? IdDocTypeDescr { get; set; }
        public DateTime? IdDocIssuingDate { get; set; }
        public DateTime? IdDocValidDate { get; set; }
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
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? RehabilitationDate { get; set; }
        public string? EcrisConvictionId { get; set; }
        public bool? Locked { get; set; }
        public bool? NoSanction { get; set; }
        public bool? PrevSuspSent { get; set; }
        public string? PrevSuspSentDescr { get; set; }
        public string? Suid { get; set; }
        public bool? EuCitizen { get; set; }
        public bool? TcnCitizen { get; set; }
        public string? BirthCityName { get; set; }
        public string? BirthMunName { get; set; }
        public string? BirthDistrictName { get; set; }
        public string? BirthCountryName { get; set; }
        public string? CsAuthorityName { get; set; }
        public string? CaseAuthName { get; set; }
        public string? DecidingAuthName { get; set; }
        public string? DecisionTypeName { get; set; }
        public string? CaseTypeName { get; set; }
        public string? BulletinAuthorityName { get; set; }
        public string? CreatedByUsername { get; set; }
        public string? UpdatedByUsername { get; set; }
        public string? IdDocCategoryName { get; set; }
    }
}