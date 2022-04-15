using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinBaseDTO : BaseDTO
    {
        public string? CsAuthorityName { get; set; }
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
        public decimal? Sex { get; set; }
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
        public string? EcrisConvictionId { get; set; }
        public AddressDTO Address { get; set; } = new AddressDTO();
        public MultipleChooseDTO Nationalities { get; set; } = new MultipleChooseDTO();
        public List<TransactionDTO<OffenceDTO>> OffancesTransactions { get; set; }
        public List<TransactionDTO<SanctionDTO>> SanctionsTransactions { get; set; }
        public List<TransactionDTO<DecisionDTO>> DecisionsTransactions { get; set; }
        public List<TransactionDTO<PersonAliasDTO>> PersonAliasTransactions { get; set; }
    }
}
