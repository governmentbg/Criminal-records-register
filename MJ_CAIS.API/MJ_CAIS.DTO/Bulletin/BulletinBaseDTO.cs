using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinBaseDTO : BaseDTO
    {
        #region Bulletin main data

        public string? RegistrationNumberDisplay { get; set; }
        public string? CsAuthorityName { get; set; }
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
        public string? CaseAuthId { get; set; }
        public string? ConvRemarks { get; set; }

        #endregion

        public PersonDTO Person { get; set; } = new PersonDTO();
        public bool? NoSanction { get; set; }
        public bool? PrevSuspSent { get; set; }
        public string? PrevSuspSentDescr { get; set; }
        public bool? Locked { get; set; }
        public List<TransactionDTO<OffenceDTO>> OffancesTransactions { get; set; }
        public List<TransactionDTO<SanctionDTO>> SanctionsTransactions { get; set; }
        public List<TransactionDTO<DecisionDTO>> DecisionsTransactions { get; set; }
    }
}
