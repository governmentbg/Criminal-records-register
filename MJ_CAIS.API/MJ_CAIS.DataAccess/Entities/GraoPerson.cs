using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GraoPerson : BaseEntity, IBaseIdEntity
    {
        public GraoPerson()
        {
            EEcrisIdentifications = new HashSet<EEcrisIdentification>();
        }

        public string? Egn { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthplaceCode { get; set; }
        public decimal? Sex { get; set; }
        public string? MothersNames { get; set; }
        public string? FathersNames { get; set; }
        public string? BirthplaceText { get; set; }
        public string Id { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<EEcrisIdentification> EEcrisIdentifications { get; set; }
    }
}
