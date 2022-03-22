using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisIdentification : BaseEntity
    {
        public string? EcrisMsgId { get; set; }
        public string? PersonId { get; set; }
        public string? GraoPersonId { get; set; }
        public decimal? Approved { get; set; }

        public virtual EEcrisMessage? EcrisMsg { get; set; }
        public virtual GraoPerson? GraoPerson { get; set; }
        public virtual PPerson? Person { get; set; }
    }
}
