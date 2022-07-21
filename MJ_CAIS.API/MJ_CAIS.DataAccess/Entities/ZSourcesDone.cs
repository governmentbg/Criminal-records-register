using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZSourcesDone
    {
        public string SourceCourt { get; set; } = null!;
        public byte? BulletinsDone { get; set; }
        public byte? ServicesDone { get; set; }
        public byte? ApplicationsDone { get; set; }
    }
}
