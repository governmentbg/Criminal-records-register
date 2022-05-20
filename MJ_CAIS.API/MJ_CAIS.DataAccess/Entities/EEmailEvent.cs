using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEmailEvent : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? EmailAddress { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? EmailStatus { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
        public byte? Attempts { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
