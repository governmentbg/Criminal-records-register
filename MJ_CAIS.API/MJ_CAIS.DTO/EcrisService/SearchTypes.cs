using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.EcrisService
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SearchWSOutputDataType
    {

        private MessageShortViewType[] messageShortViewListField;

        private int numberOfResultsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Message", Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0", IsNullable = false)]
        public MessageShortViewType[] MessageShortViewList
        {
            get
            {
                return this.messageShortViewListField;
            }
            set
            {
                this.messageShortViewListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int NumberOfResults
        {
            get
            {
                return this.numberOfResultsField;
            }
            set
            {
                this.numberOfResultsField = value;
            }
        }
    }

}
