using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinBaseDTO : BaseDTO
    {
        #region Bulletin main data

        public string? RegistrationNumber { get; set; }
        public string? CsAuthorityName { get; set; }
        public decimal? SequentialIndex { get; set; }
        public string? StatusId { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string? EcrisConvictionId { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? BulletinType { get; set; }
        public string? BulletinAuthorityId { get; set; }
        public DateTime? BulletinCreateDate { get; set; }
        public string? CreatedByNames { get; set; }
        public string? CreatedByPosition { get; set; }
        public string? ApprovedByNames { get; set; }
        public string? ApprovedByPosition { get; set; }

        #endregion

        #region Person 

        public string? PersonId { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public string? FullnameLat { get; set; }
        public List<TransactionDTO<PersonAliasDTO>> PersonAliasTransactions { get; set; }
        public decimal? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? Ln { get; set; }
        public AddressDTO BirthPlace { get; set; } = new AddressDTO();
        public MultipleChooseDTO Nationalities { get; set; } = new MultipleChooseDTO();
        public string? AfisNumber { get; set; }
        public string? IdDocNumber { get; set; }
        public string? IdDocCategoryId { get; set; }
        public string? IdDocTypeDescr { get; set; }
        public string? IdDocIssuingAuthority { get; set; }
        public DateTime? IdDocIssuingDate { get; set; }
        public DateTime? IdDocValidDate { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? FatherFullname { get; set; }

        #endregion

        #region Decision data

        public string? DecisionTypeId { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecidingAuthId { get; set; }
        public string? DecisionEcli { get; set; }

        #endregion

        #region Case data

        public string? CaseTypeId { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public decimal? CaseAuthId { get; set; }
        public string? ConvRemarks { get; set; }

        #endregion

        public bool? NoSanction { get; set; }
        public bool? PrevSuspSent { get; set; }
        public string? PrevSuspSentDescr { get; set; }
        public bool? Locked { get; set; }
        public List<TransactionDTO<OffenceDTO>> OffancesTransactions { get; set; }
        public List<TransactionDTO<SanctionDTO>> SanctionsTransactions { get; set; }
        public List<TransactionDTO<DecisionDTO>> DecisionsTransactions { get; set; }
    }
}
