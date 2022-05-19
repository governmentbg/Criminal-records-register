using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AApplicationStatus
    {
        public AApplicationStatus()
        {
            ACertificates = new HashSet<ACertificate>();
            AStatusHes = new HashSet<AStatusH>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? StatusType { get; set; }
        public string? Descr { get; set; }

        public virtual ICollection<ACertificate> ACertificates { get; set; }
        public virtual ICollection<AStatusH> AStatusHes { get; set; }
    }
}
