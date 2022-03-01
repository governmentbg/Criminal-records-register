using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisIdentification
    {
        public string Id { get; set; } = null!;
        public string? EcrisMsgId { get; set; }
        public string? PersonId { get; set; }
        public string? GraoPersonId { get; set; }

        public virtual EEcrisMessage? EcrisMsg { get; set; }
        public virtual GraoPerson? GraoPerson { get; set; }
        public virtual PPerson? Person { get; set; }
    }
}
