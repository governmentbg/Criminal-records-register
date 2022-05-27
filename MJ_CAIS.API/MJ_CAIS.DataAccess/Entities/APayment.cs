using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class APayment : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Status { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ApplicationId { get; set; }
        public string? WApplicationId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual AApplication? Application { get; set; }
        public virtual WApplication? WApplication { get; set; }
    }
}
