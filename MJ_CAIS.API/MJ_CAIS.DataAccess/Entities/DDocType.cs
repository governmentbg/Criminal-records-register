﻿using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class DDocType
    {
        public DDocType()
        {
            DDocuments = new HashSet<DDocument>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Xslt { get; set; }

        public virtual ICollection<DDocument> DDocuments { get; set; }
    }
}
