using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEdeliveryMsg : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public DateTime? SentDate { get; set; }
        public bool? HasError { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? EmailAddress { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? StackTrace { get; set; }
        public string? Error { get; set; }
        public string? Status { get; set; }
        public byte? Attempts { get; set; }
        public string? CertificateId { get; set; }

        public virtual ACertificate? Certificate { get; set; }
    }
}
