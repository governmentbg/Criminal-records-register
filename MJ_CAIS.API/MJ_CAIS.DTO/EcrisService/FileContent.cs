using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MJ_CAIS.DTO.EcrisService
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/file-monitor-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute("FileContent", Namespace = "http://ec.europa.eu/ECRIS-RI/file-monitor-v1.0", IsNullable = false)]
    public partial class FileContentType
    {

        private System.DateTime metaDataTimeStampField;

        private AbstractMessageType ecrisMessageField;

        /// <remarks/>
        public System.DateTime MetaDataTimeStamp
        {
            get
            {
                return this.metaDataTimeStampField;
            }
            set
            {
                this.metaDataTimeStampField = value;
            }
        }

        /// <remarks/>
        public AbstractMessageType EcrisMessage
        {
            get
            {
                return this.ecrisMessageField;
            }
            set
            {
                this.ecrisMessageField = value;
            }
        }
    }
}
