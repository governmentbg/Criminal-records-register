using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WReport : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public string? Pid { get; set; }
        public string? PidType { get; set; }
        public string? ResultType { get; set; }
        public decimal? ApiServiceCallId { get; set; }
        public string? CServiceUri { get; set; }
        public string? CServiceType { get; set; }
        public string? CEmplId { get; set; }
        public string? CEmpNames { get; set; }
        public string? CEmpAddId { get; set; }
        public string? CEmpPos { get; set; }
        public string? CRespPersId { get; set; }
        public string? CLawReason { get; set; }
        public string? CRemark { get; set; }
        public string? CAdministrationOid { get; set; }
        public string? CAdministrationName { get; set; }
        public string? ResultId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
