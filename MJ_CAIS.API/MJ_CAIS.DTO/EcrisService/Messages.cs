using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MJ_CAIS.DTO.EcrisService
{
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoMessageShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResponseMessageShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MessageRelatedToRequestShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestMessageShortViewType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class MessageShortViewType : AbstractMessageType
    {
        [XmlIgnoreAttribute]
        public string SerializedXMLFromService { get; set; }

        private MessageShortViewPersonType messageShortViewPersonField;

        private StrictDateTimeType messageDeadlineField;

        private StrictDateTimeType messageLastUpdateDateField;

        private string messageLastModifiedByUserField;

        private bool messageHasFunctionalErrorsField;

        private bool messageHasNISTAttachmentField;

        private bool messageIsRepliedField;

        private bool messageTransmissionProblemField;

        private bool messageIsArchivedField;

        private bool messageIsCancelledField;

        private IdentifiableFolderType containerFolderField;

        private string authoringLanguageField;

        public MessageShortViewType()
        {
            this.messageHasFunctionalErrorsField = false;
            this.messageHasNISTAttachmentField = false;
            this.messageIsRepliedField = false;
            this.messageTransmissionProblemField = false;
            this.messageIsArchivedField = false;
            this.messageIsCancelledField = false;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MessageShortViewPersonType MessageShortViewPerson
        {
            get
            {
                return this.messageShortViewPersonField;
            }
            set
            {
                this.messageShortViewPersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StrictDateTimeType MessageDeadline
        {
            get
            {
                return this.messageDeadlineField;
            }
            set
            {
                this.messageDeadlineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public StrictDateTimeType MessageLastUpdateDate
        {
            get
            {
                return this.messageLastUpdateDateField;
            }
            set
            {
                this.messageLastUpdateDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string MessageLastModifiedByUser
        {
            get
            {
                return this.messageLastModifiedByUserField;
            }
            set
            {
                this.messageLastModifiedByUserField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MessageHasFunctionalErrors
        {
            get
            {
                return this.messageHasFunctionalErrorsField;
            }
            set
            {
                this.messageHasFunctionalErrorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MessageHasNISTAttachment
        {
            get
            {
                return this.messageHasNISTAttachmentField;
            }
            set
            {
                this.messageHasNISTAttachmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MessageIsReplied
        {
            get
            {
                return this.messageIsRepliedField;
            }
            set
            {
                this.messageIsRepliedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MessageTransmissionProblem
        {
            get
            {
                return this.messageTransmissionProblemField;
            }
            set
            {
                this.messageTransmissionProblemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MessageIsArchived
        {
            get
            {
                return this.messageIsArchivedField;
            }
            set
            {
                this.messageIsArchivedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MessageIsCancelled
        {
            get
            {
                return this.messageIsCancelledField;
            }
            set
            {
                this.messageIsCancelledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public IdentifiableFolderType ContainerFolder
        {
            get
            {
                return this.containerFolderField;
            }
            set
            {
                this.containerFolderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string AuthoringLanguage
        {
            get
            {
                return this.authoringLanguageField;
            }
            set
            {
                this.authoringLanguageField = value;
            }
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AbstractBusinessMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CancellationMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FunctionalErrorMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoResponseMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationResponseMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestResponseMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestDeadlineMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MessageShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoMessageShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResponseMessageShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MessageRelatedToRequestShortViewType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestMessageShortViewType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractMessageType : IdentifiableMessageType
    // public abstract partial class AbstractMessageType : IdentifiableMessageType
    {

        private RestrictedIdentifiableMessageType messageResponseToField;

        private MemberStateCodeType messageSendingMemberStateField;

        private bool messageSendingMemberStateFieldSpecified;

        private MemberStateCodeType[] messageReceivingMemberStateField;

        private EcrisMessageType messageTypeField;

        private bool messageTypeFieldSpecified;

        private System.DateTime messageSenderTimestampField;

        private bool messageSenderTimestampFieldSpecified;

        private System.DateTime messageVersionTimestampField;

        private bool messageVersionTimestampFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RestrictedIdentifiableMessageType MessageResponseTo
        {
            get
            {
                return this.messageResponseToField;
            }
            set
            {
                this.messageResponseToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MemberStateCodeType MessageSendingMemberState
        {
            get
            {
                return this.messageSendingMemberStateField;
            }
            set
            {
                this.messageSendingMemberStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageSendingMemberStateSpecified
        {
            get
            {
                return this.messageSendingMemberStateFieldSpecified;
            }
            set
            {
                this.messageSendingMemberStateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MessageReceivingMemberState", Order = 2)]
        public MemberStateCodeType[] MessageReceivingMemberState
        {
            get
            {
                return this.messageReceivingMemberStateField;
            }
            set
            {
                this.messageReceivingMemberStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public EcrisMessageType MessageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageTypeSpecified
        {
            get
            {
                return this.messageTypeFieldSpecified;
            }
            set
            {
                this.messageTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public System.DateTime MessageSenderTimestamp
        {
            get
            {
                return this.messageSenderTimestampField;
            }
            set
            {
                this.messageSenderTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageSenderTimestampSpecified
        {
            get
            {
                return this.messageSenderTimestampFieldSpecified;
            }
            set
            {
                this.messageSenderTimestampFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public System.DateTime MessageVersionTimestamp
        {
            get
            {
                return this.messageVersionTimestampField;
            }
            set
            {
                this.messageVersionTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageVersionTimestampSpecified
        {
            get
            {
                return this.messageVersionTimestampFieldSpecified;
            }
            set
            {
                this.messageVersionTimestampFieldSpecified = value;
            }
        }
    }

}
