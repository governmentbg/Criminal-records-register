using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZService
    {
        public string ServiceId { get; set; } = null!;
        public string CreatorId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string ModifierId { get; set; } = null!;
        public DateTime ModifyDate { get; set; }
        public string SiteId { get; set; } = null!;
        public DateTime? ServiceDate { get; set; }
        public decimal? RequestType { get; set; }
        public decimal? ServiceStatus { get; set; }
        public DateTime? StatusDate { get; set; }
        public byte? ServiceResult { get; set; }
        public string? Purpose { get; set; }
        public string? PersonId { get; set; }
        public string? Judge { get; set; }
        public DateTime? JudgeDate { get; set; }
        public string? UserId { get; set; }
        public string? PersonRequest { get; set; }
        public long? CertificateNumber { get; set; }
        public string? MessageId { get; set; }
        public decimal? CityCodeAnswer { get; set; }
        public string? AddressAnswer { get; set; }
        public string? Reabilitation { get; set; }
        public string? XmlReg { get; set; }
        public string? XmlAnswer { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? R1 { get; set; }
        public string? R2 { get; set; }
        public long? ApplicationNumber { get; set; }
        public DateTime? ApplicationNumberDate { get; set; }
    }
}
