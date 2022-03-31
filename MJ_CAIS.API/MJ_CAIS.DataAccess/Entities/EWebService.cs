using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EWebService : BaseEntity
    {
        public EWebService()
        {
            EWebRequests = new HashSet<EWebRequest>();
        }

        public string? TypeCode { get; set; }
        public string? Name { get; set; }
        public string? WebServiceName { get; set; }

        public virtual ICollection<EWebRequest> EWebRequests { get; set; }
    }
}
