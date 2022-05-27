using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EPaymentNotification : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public DateTime? LogDate { get; set; }
        public string? Status { get; set; }
        public string? NotificatonData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
