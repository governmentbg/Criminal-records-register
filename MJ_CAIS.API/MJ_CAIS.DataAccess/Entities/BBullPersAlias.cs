using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BBullPersAlias : BaseEntity
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? Type { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
    }
}
