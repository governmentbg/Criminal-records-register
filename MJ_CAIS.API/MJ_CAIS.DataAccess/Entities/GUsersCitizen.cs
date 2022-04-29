using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GUsersCitizen : BaseEntity
    {
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
