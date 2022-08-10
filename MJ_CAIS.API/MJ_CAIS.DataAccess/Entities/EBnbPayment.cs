using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EBnbPayment : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public decimal? SentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ImportDate { get; set; }
        public bool? PaymentConfirmed { get; set; }
        public string? EntryType { get; set; }
        public string? DestinationIban { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? DocumentNumber { get; set; }
        public string? WritingType { get; set; }
        public decimal? Amount { get; set; }
        public string? CorrIban { get; set; }
        public string? CorrPaymentType { get; set; }
        public string? ContragentName { get; set; }
        public string? PaymentReason { get; set; }
        public string? PaymentReasonDetails { get; set; }
        public string? DocumentCode { get; set; }
        public string? AddInfoDocType { get; set; }
        public string? AddInfoDocNum { get; set; }
        public string? AddInfoDocDate { get; set; }
        public string? AddInfoPeriodFrom { get; set; }
        public string? AddInfoPeriodTo { get; set; }
        public string? AddInfoPersonName { get; set; }
        public string? AddInfoPersonBulstat { get; set; }
        public string? AddInfoPersonEgn { get; set; }
        public string? AddInfoPersonLnch { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CorrReference { get; set; }
    }
}
