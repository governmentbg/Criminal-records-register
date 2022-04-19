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
    public partial class QueryType
    {

        private string queryNameField;

        private QueryTypeQueryParameters queryParametersField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string QueryName
        {
            get
            {
                return this.queryNameField;
            }
            set
            {
                this.queryNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public QueryTypeQueryParameters QueryParameters
        {
            get
            {
                return this.queryParametersField;
            }
            set
            {
                this.queryParametersField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class BirthDateQueryParameter : AbstractDateQueryParameter
    {

        private DateRange birthDateRangeParameterField;

        private DateType birthDateTypeParameterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public DateRange BirthDateRangeParameter
        {
            get
            {
                return this.birthDateRangeParameterField;
            }
            set
            {
                this.birthDateRangeParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public DateType BirthDateTypeParameter
        {
            get
            {
                return this.birthDateTypeParameterField;
            }
            set
            {
                this.birthDateTypeParameterField = value;
            }
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BirthDateQueryParameter))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DateStrictRangeQueryParameter))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public abstract partial class AbstractDateQueryParameter
    {

        private AbstractDateQueryParameterDateParameterType dateParameterTypeField;

        private string intervalParameterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractDateQueryParameterDateParameterType DateParameterType
        {
            get
            {
                return this.dateParameterTypeField;
            }
            set
            {
                this.dateParameterTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 1)]
        public string IntervalParameter
        {
            get
            {
                return this.intervalParameterField;
            }
            set
            {
                this.intervalParameterField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum AbstractDateQueryParameterDateParameterType
    {

        /// <remarks/>
        Today,

        /// <remarks/>
        CurrentWeek,

        /// <remarks/>
        WithinLast,

        /// <remarks/>
        WithinNext,

        /// <remarks/>
        Range,

        /// <remarks/>
        Date,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class QueryTypeQueryParameters
    {

        private string forenameQueryParameterField;

        private string surnameQueryParameterField;

        private string personIdentityNumberQueryParameterField;

        private BirthDateQueryParameter personDateOfBirthQueryParameterField;

        private CountryExternalReferenceType[] personCountryOfBirthQueryParameterField;

        private QueryTypeQueryParametersPersonTownOfBirthQueryParameter[] personTownOfBirthQueryParameterField;

        private int personSexQueryParameterField;

        private bool personSexQueryParameterFieldSpecified;

        private CountryExternalReferenceType[] personNationalityQueryParameterField;

        private QueryTypeQueryParametersMemberStateQueryParameter memberStateQueryParameterField;

        private DateStrictRangeQueryParameter messageDeadlineQueryParameterField;

        private MessageDateQueryParameterType messageDateQueryParameterField;

        private string convictionIdentifierQueryParameterField;

        private string[] folderQueryParameterField;

        private YesNoUnknownStringEnumerationType[] requestUrgencyQueryParameterField;

        private RequestPurposeExternalReferenceType[] requestPurposeCategoryQueryParameterField;

        private EcrisMessageTypeOrAlias[] messageTypeQueryParameterField;

        private string lastModifiedByUserQueryParameterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ForenameQueryParameter
        {
            get
            {
                return this.forenameQueryParameterField;
            }
            set
            {
                this.forenameQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string SurnameQueryParameter
        {
            get
            {
                return this.surnameQueryParameterField;
            }
            set
            {
                this.surnameQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string PersonIdentityNumberQueryParameter
        {
            get
            {
                return this.personIdentityNumberQueryParameterField;
            }
            set
            {
                this.personIdentityNumberQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public BirthDateQueryParameter PersonDateOfBirthQueryParameter
        {
            get
            {
                return this.personDateOfBirthQueryParameterField;
            }
            set
            {
                this.personDateOfBirthQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonCountryOfBirthQueryParameter", Order = 4)]
        public CountryExternalReferenceType[] PersonCountryOfBirthQueryParameter
        {
            get
            {
                return this.personCountryOfBirthQueryParameterField;
            }
            set
            {
                this.personCountryOfBirthQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonTownOfBirthQueryParameter", Order = 5)]
        public QueryTypeQueryParametersPersonTownOfBirthQueryParameter[] PersonTownOfBirthQueryParameter
        {
            get
            {
                return this.personTownOfBirthQueryParameterField;
            }
            set
            {
                this.personTownOfBirthQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public int PersonSexQueryParameter
        {
            get
            {
                return this.personSexQueryParameterField;
            }
            set
            {
                this.personSexQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PersonSexQueryParameterSpecified
        {
            get
            {
                return this.personSexQueryParameterFieldSpecified;
            }
            set
            {
                this.personSexQueryParameterFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonNationalityQueryParameter", Order = 7)]
        public CountryExternalReferenceType[] PersonNationalityQueryParameter
        {
            get
            {
                return this.personNationalityQueryParameterField;
            }
            set
            {
                this.personNationalityQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public QueryTypeQueryParametersMemberStateQueryParameter MemberStateQueryParameter
        {
            get
            {
                return this.memberStateQueryParameterField;
            }
            set
            {
                this.memberStateQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public DateStrictRangeQueryParameter MessageDeadlineQueryParameter
        {
            get
            {
                return this.messageDeadlineQueryParameterField;
            }
            set
            {
                this.messageDeadlineQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public MessageDateQueryParameterType MessageDateQueryParameter
        {
            get
            {
                return this.messageDateQueryParameterField;
            }
            set
            {
                this.messageDateQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string ConvictionIdentifierQueryParameter
        {
            get
            {
                return this.convictionIdentifierQueryParameterField;
            }
            set
            {
                this.convictionIdentifierQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FolderQueryParameter", Order = 12)]
        public string[] FolderQueryParameter
        {
            get
            {
                return this.folderQueryParameterField;
            }
            set
            {
                this.folderQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestUrgencyQueryParameter", Order = 13)]
        public YesNoUnknownStringEnumerationType[] RequestUrgencyQueryParameter
        {
            get
            {
                return this.requestUrgencyQueryParameterField;
            }
            set
            {
                this.requestUrgencyQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestPurposeCategoryQueryParameter", Order = 14)]
        public RequestPurposeExternalReferenceType[] RequestPurposeCategoryQueryParameter
        {
            get
            {
                return this.requestPurposeCategoryQueryParameterField;
            }
            set
            {
                this.requestPurposeCategoryQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MessageTypeQueryParameter", Order = 15)]
        public EcrisMessageTypeOrAlias[] MessageTypeQueryParameter
        {
            get
            {
                return this.messageTypeQueryParameterField;
            }
            set
            {
                this.messageTypeQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public string LastModifiedByUserQueryParameter
        {
            get
            {
                return this.lastModifiedByUserQueryParameterField;
            }
            set
            {
                this.lastModifiedByUserQueryParameterField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class QueryTypeQueryParametersPersonTownOfBirthQueryParameter
    {

        private CityExternalReferenceType placeTownReferenceField;

        private string placeTownNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public CityExternalReferenceType PlaceTownReference
        {
            get
            {
                return this.placeTownReferenceField;
            }
            set
            {
                this.placeTownReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string PlaceTownName
        {
            get
            {
                return this.placeTownNameField;
            }
            set
            {
                this.placeTownNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class QueryTypeQueryParametersMemberStateQueryParameter
    {

        private MemberStateCodeType[] memberStateCodesField;

        private QueryTypeQueryParametersMemberStateQueryParameterDestination destinationField;

        private bool destinationFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MemberStateCodes", Order = 0)]
        public MemberStateCodeType[] MemberStateCodes
        {
            get
            {
                return this.memberStateCodesField;
            }
            set
            {
                this.memberStateCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public QueryTypeQueryParametersMemberStateQueryParameterDestination Destination
        {
            get
            {
                return this.destinationField;
            }
            set
            {
                this.destinationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DestinationSpecified
        {
            get
            {
                return this.destinationFieldSpecified;
            }
            set
            {
                this.destinationFieldSpecified = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum QueryTypeQueryParametersMemberStateQueryParameterDestination
    {

        /// <remarks/>
        SENT,

        /// <remarks/>
        RECEIVED,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SENT-RECEIVED")]
        SENTRECEIVED,
    }

    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DateStrictRangeQueryParameter : AbstractDateQueryParameter
    {

        private StrictDateRange dateRangeParameterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateRange DateRangeParameter
        {
            get
            {
                return this.dateRangeParameterField;
            }
            set
            {
                this.dateRangeParameterField = value;
            }
        }
    }
    /// <remarks/>
   
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MessageDateQueryParameterType
    {

        private MessageDateTypeEnumeration dateTypeField;

        private DateStrictRangeQueryParameter dateValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MessageDateTypeEnumeration DateType
        {
            get
            {
                return this.dateTypeField;
            }
            set
            {
                this.dateTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public DateStrictRangeQueryParameter DateValue
        {
            get
            {
                return this.dateValueField;
            }
            set
            {
                this.dateValueField = value;
            }
        }
    }
}
