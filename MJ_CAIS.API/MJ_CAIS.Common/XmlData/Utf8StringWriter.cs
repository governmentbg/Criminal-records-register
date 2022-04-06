using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Common.XmlData
{
    public class Utf8StringWriter : StringWriter
    {
        public Utf8StringWriter(StringBuilder builder) : base(builder) { }

        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
