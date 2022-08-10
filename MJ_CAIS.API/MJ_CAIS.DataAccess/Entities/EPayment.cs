using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EPayment : BaseEntity, IBaseIdEntity
    {
        public EPayment()
        {
            APayments = new HashSet<APayment>();
        }

        public string Id { get; set; } = null!;
        public string? MerchantId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? PaymentStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? BnbPayId { get; set; }

        public virtual EBnbPayment? BnbPay { get; set; }
        public virtual ICollection<APayment> APayments { get; set; }
    }
}
