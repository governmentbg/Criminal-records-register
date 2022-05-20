using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EWebService : BaseEntity, IBaseIdEntity
    {
        public EWebService()
        {
            EWebRequests = new HashSet<EWebRequest>();
        }

        public string Id { get; set; } = null!;
        public string? TypeCode { get; set; }
        public string? Name { get; set; }
        public string? WebServiceName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<EWebRequest> EWebRequests { get; set; }
    }
}
