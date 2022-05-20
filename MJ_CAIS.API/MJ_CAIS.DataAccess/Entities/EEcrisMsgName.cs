using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMsgName : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? LangCode { get; set; }
        public string? EEcrisMsgId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual EEcrisMessage? EEcrisMsg { get; set; }
    }
}
