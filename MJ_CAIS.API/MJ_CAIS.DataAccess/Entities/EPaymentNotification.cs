using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EPaymentNotification : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? PaymentId { get; set; }
        public string? LogDate { get; set; }
        public string? EncodedText { get; set; }
        public string? DecodedText { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
