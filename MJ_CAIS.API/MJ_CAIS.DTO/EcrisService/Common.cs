

using System.Xml.Serialization;

namespace MJ_CAIS.DTO.EcrisService
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ServerErrorFaultType : AbstractFaultType
    {

        private string serverErrorFaultCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ServerErrorFaultCode
        {
            get
            {
                return this.serverErrorFaultCodeField;
            }
            set
            {
                this.serverErrorFaultCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OperationFaultType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ServerErrorFaultType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AuthenticationFaultType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public abstract partial class AbstractFaultType
    {

        private string messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "MonthDayType", Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0")]
    public partial class MonthDayType1
    {

        private string dateMonthField;

        private string dateDayField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gMonth", Order = 0)]
        public string DateMonth
        {
            get
            {
                return this.dateMonthField;
            }
            set
            {
                this.dateMonthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gDay", Order = 1)]
        public string DateDay
        {
            get
            {
                return this.dateDayField;
            }
            set
            {
                this.dateDayField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "DateType", Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0")]
    public partial class DateType1
    {

        private string dateYearField;

        private MonthDayType1 dateMonthDayField;

        public DateType1()
        {
            this.dateYearField = "1800";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear", Order = 0)]
        public string DateYear
        {
            get
            {
                return this.dateYearField;
            }
            set
            {
                this.dateYearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MonthDayType1 DateMonthDay
        {
            get
            {
                return this.dateMonthDayField;
            }
            set
            {
                this.dateMonthDayField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PositiveDecimalType", Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0")]
    public partial class PositiveDecimalType1
    {

        private string positiveDecimalUnitField;

        private string positiveDecimalFractionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger", Order = 0)]
        public string PositiveDecimalUnit
        {
            get
            {
                return this.positiveDecimalUnitField;
            }
            set
            {
                this.positiveDecimalUnitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 1)]
        public string PositiveDecimalFraction
        {
            get
            {
                return this.positiveDecimalFractionField;
            }
            set
            {
                this.positiveDecimalFractionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AbstractRelationshipType", Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0")]
    public abstract partial class AbstractRelationshipType1
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class StatisticsPeriodType
    {

        private StatisticsFixedPeriodType fixedPeriodField;

        private StatisticsCustomPeriodType customPeriodField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StatisticsFixedPeriodType FixedPeriod
        {
            get
            {
                return this.fixedPeriodField;
            }
            set
            {
                this.fixedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StatisticsCustomPeriodType CustomPeriod
        {
            get
            {
                return this.customPeriodField;
            }
            set
            {
                this.customPeriodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class StatisticsFixedPeriodType : AbstractStatisticsPeriodType
    {

        private string yearField;

        private string monthField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear", Order = 0)]
        public string Year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gMonth", Order = 1)]
        public string Month
        {
            get
            {
                return this.monthField;
            }
            set
            {
                this.monthField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StatisticsCustomPeriodType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StatisticsFixedPeriodType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractStatisticsPeriodType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class StatisticsCustomPeriodType : AbstractStatisticsPeriodType
    {

        private StrictDateType fromDateField;

        private StrictDateType toDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateType FromDate
        {
            get
            {
                return this.fromDateField;
            }
            set
            {
                this.fromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StrictDateType ToDate
        {
            get
            {
                return this.toDateField;
            }
            set
            {
                this.toDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class StrictDateType
    {

        private string functionalErrorReferenceIdentifierField;

        private System.DateTime valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "date")]
        public System.DateTime Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StatisticDefinitionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FaultType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FunctionalErrorType1))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionToSanctionsRelationshipType1))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(IdentificationDocumentCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AbstractCategorisedType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestingAuthorityType1))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DecisionChangeType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionNatureType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionTypeOfSuspensionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfCompletionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionAlternativeType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfParticipationType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AbstractCommonCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestPurposeCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CityType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountrySubdivisionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CurrencyType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LanguageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CentralAuthorityType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "EntityType", Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public abstract partial class EntityType1
    {

        private System.DateTime validFromField;

        private System.DateTime validToField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation1[] nameField;

        public EntityType1()
        {
            this.validFromField = new System.DateTime(567709344000000000);
            this.validToField = new System.DateTime(946391904000000000);
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 0)]
        public System.DateTime ValidFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 1)]
        public System.DateTime ValidTo
        {
            get
            {
                return this.validToField;
            }
            set
            {
                this.validToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation1[] Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0")]
    public partial class MultilingualTextType400CharsMultilingualTextLinguisticRepresentation1
    {

        private string languageCodeField;

        private bool translatedField;

        private bool translatedFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool translated
        {
            get
            {
                return this.translatedField;
            }
            set
            {
                this.translatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool translatedSpecified
        {
            get
            {
                return this.translatedFieldSpecified;
            }
            set
            {
                this.translatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/statistics-v1.0")]
    public partial class StatisticDefinitionType : EntityType1
    {

        private string statisticDefinitionTechnicalIdentifierField;

        private string statisticDefinitionCategoryCodeField;

        private string statisticDefinitionCategorySubCategoryCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string StatisticDefinitionTechnicalIdentifier
        {
            get
            {
                return this.statisticDefinitionTechnicalIdentifierField;
            }
            set
            {
                this.statisticDefinitionTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string StatisticDefinitionCategoryCode
        {
            get
            {
                return this.statisticDefinitionCategoryCodeField;
            }
            set
            {
                this.statisticDefinitionCategoryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string StatisticDefinitionCategorySubCategoryCode
        {
            get
            {
                return this.statisticDefinitionCategorySubCategoryCodeField;
            }
            set
            {
                this.statisticDefinitionCategorySubCategoryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class FaultType : EntityType1
    {

        private string faultCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "NCName", Order = 0)]
        public string FaultCode
        {
            get
            {
                return this.faultCodeField;
            }
            set
            {
                this.faultCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "FunctionalErrorType", Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class FunctionalErrorType1 : EntityType1
    {

        private string functionalErrorCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string FunctionalErrorCode
        {
            get
            {
                return this.functionalErrorCodeField;
            }
            set
            {
                this.functionalErrorCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class RequestAdditionalInfoResponseType : EntityType1
    {

        private string requestAdditionalInfoTypeTechnicalIdentifierField;

        private string requestAdditionalInfoTypeCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string RequestAdditionalInfoTypeTechnicalIdentifier
        {
            get
            {
                return this.requestAdditionalInfoTypeTechnicalIdentifierField;
            }
            set
            {
                this.requestAdditionalInfoTypeTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 1)]
        public string RequestAdditionalInfoTypeCode
        {
            get
            {
                return this.requestAdditionalInfoTypeCodeField;
            }
            set
            {
                this.requestAdditionalInfoTypeCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class NotificationResponseType : EntityType1
    {

        private string notificationResponseTypeTechnicalIdentifierField;

        private string notificationResponseTypeCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string NotificationResponseTypeTechnicalIdentifier
        {
            get
            {
                return this.notificationResponseTypeTechnicalIdentifierField;
            }
            set
            {
                this.notificationResponseTypeTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 1)]
        public string NotificationResponseTypeCode
        {
            get
            {
                return this.notificationResponseTypeCodeField;
            }
            set
            {
                this.notificationResponseTypeCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SanctionToSanctionsRelationshipType", Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class SanctionToSanctionsRelationshipType1 : EntityType1
    {

        private string sanctionToSanctionsRelationTypeTechnicalIdentifierField;

        private string sanctionToSanctionsRelationTypeCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string SanctionToSanctionsRelationTypeTechnicalIdentifier
        {
            get
            {
                return this.sanctionToSanctionsRelationTypeTechnicalIdentifierField;
            }
            set
            {
                this.sanctionToSanctionsRelationTypeTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string SanctionToSanctionsRelationTypeCode
        {
            get
            {
                return this.sanctionToSanctionsRelationTypeCodeField;
            }
            set
            {
                this.sanctionToSanctionsRelationTypeCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class RequestResponseType : EntityType1
    {

        private string requestResponseTypeTechnicalIdentifierField;

        private string requestResponseTypeCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string RequestResponseTypeTechnicalIdentifier
        {
            get
            {
                return this.requestResponseTypeTechnicalIdentifierField;
            }
            set
            {
                this.requestResponseTypeTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 1)]
        public string RequestResponseTypeCode
        {
            get
            {
                return this.requestResponseTypeCodeField;
            }
            set
            {
                this.requestResponseTypeCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class IdentificationDocumentCategoryType : EntityType1
    {

        private string identificationDocumentCategoryTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string IdentificationDocumentCategoryTechnicalIdentifier
        {
            get
            {
                return this.identificationDocumentCategoryTechnicalIdentifierField;
            }
            set
            {
                this.identificationDocumentCategoryTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestingAuthorityType1))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DecisionChangeType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionNatureType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionTypeOfSuspensionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfCompletionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionAlternativeType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfParticipationType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public abstract partial class AbstractCategorisedType : EntityType1
    {

        private string categoryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequestingAuthorityType", Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class RequestingAuthorityType1 : AbstractCategorisedType
    {

        private string requestingAuthorityTypeTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string RequestingAuthorityTypeTechnicalIdentifier
        {
            get
            {
                return this.requestingAuthorityTypeTechnicalIdentifierField;
            }
            set
            {
                this.requestingAuthorityTypeTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class DecisionChangeType : AbstractCategorisedType
    {

        private string decisionChangeTypeTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string DecisionChangeTypeTechnicalIdentifier
        {
            get
            {
                return this.decisionChangeTypeTechnicalIdentifierField;
            }
            set
            {
                this.decisionChangeTypeTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class SanctionNatureType : AbstractCategorisedType
    {

        private string sanctionNatureTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string SanctionNatureTechnicalIdentifier
        {
            get
            {
                return this.sanctionNatureTechnicalIdentifierField;
            }
            set
            {
                this.sanctionNatureTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class SanctionTypeOfSuspensionType : AbstractCategorisedType
    {

        private string sanctionTypeOfSuspensionTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string SanctionTypeOfSuspensionTechnicalIdentifier
        {
            get
            {
                return this.sanctionTypeOfSuspensionTechnicalIdentifierField;
            }
            set
            {
                this.sanctionTypeOfSuspensionTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class OffenceLevelOfCompletionType : AbstractCategorisedType
    {

        private string offenceLevelOfCompletionTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string OffenceLevelOfCompletionTechnicalIdentifier
        {
            get
            {
                return this.offenceLevelOfCompletionTechnicalIdentifierField;
            }
            set
            {
                this.offenceLevelOfCompletionTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class SanctionAlternativeType : AbstractCategorisedType
    {

        private string sanctionAlternativeTypeTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string SanctionAlternativeTypeTechnicalIdentifier
        {
            get
            {
                return this.sanctionAlternativeTypeTechnicalIdentifierField;
            }
            set
            {
                this.sanctionAlternativeTypeTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class OffenceLevelOfParticipationType : AbstractCategorisedType
    {

        private string offenceLevelOfParticipationTechnicalIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string OffenceLevelOfParticipationTechnicalIdentifier
        {
            get
            {
                return this.offenceLevelOfParticipationTechnicalIdentifierField;
            }
            set
            {
                this.offenceLevelOfParticipationTechnicalIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestPurposeCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionCategoryType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceCategoryType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public abstract partial class AbstractCommonCategoryType : EntityType1
    {

        private bool categoryIsOpenField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool CategoryIsOpen
        {
            get
            {
                return this.categoryIsOpenField;
            }
            set
            {
                this.categoryIsOpenField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class RequestPurposeCategoryType : AbstractCommonCategoryType
    {

        private string requestPurposeTechnicalIdentifierField;

        private string requestPurposeCategoryCodeField;

        private string requestPurposeSubcategoryCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string RequestPurposeTechnicalIdentifier
        {
            get
            {
                return this.requestPurposeTechnicalIdentifierField;
            }
            set
            {
                this.requestPurposeTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string RequestPurposeCategoryCode
        {
            get
            {
                return this.requestPurposeCategoryCodeField;
            }
            set
            {
                this.requestPurposeCategoryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string RequestPurposeSubcategoryCode
        {
            get
            {
                return this.requestPurposeSubcategoryCodeField;
            }
            set
            {
                this.requestPurposeSubcategoryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class SanctionCategoryType : AbstractCommonCategoryType
    {

        private string sanctionTechnicalIdentifierField;

        private string sanctionCategoryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string SanctionTechnicalIdentifier
        {
            get
            {
                return this.sanctionTechnicalIdentifierField;
            }
            set
            {
                this.sanctionTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string SanctionCategory
        {
            get
            {
                return this.sanctionCategoryField;
            }
            set
            {
                this.sanctionCategoryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class OffenceCategoryType : AbstractCommonCategoryType
    {

        private string offenceTechnicalIdentifierField;

        private string offenceCategoryCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string OffenceTechnicalIdentifier
        {
            get
            {
                return this.offenceTechnicalIdentifierField;
            }
            set
            {
                this.offenceTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string OffenceCategoryCode
        {
            get
            {
                return this.offenceCategoryCodeField;
            }
            set
            {
                this.offenceCategoryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class CityType : EntityType1
    {

        private string cityTechnicalIdentifierField;

        private string cityCountryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string CityTechnicalIdentifier
        {
            get
            {
                return this.cityTechnicalIdentifierField;
            }
            set
            {
                this.cityTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 1)]
        public string CityCountry
        {
            get
            {
                return this.cityCountryField;
            }
            set
            {
                this.cityCountryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class CountrySubdivisionType : EntityType1
    {

        private string countrySubdivisionTechnicalIdentifierField;

        private string countrySubdivisionCodeField;

        private CountrySubdivisionTypeCountrySubdivisionType countrySubdivisionType1Field;

        private string countrySubdivisionMemberStateField;

        private string countrySubdivisionToCountryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string CountrySubdivisionTechnicalIdentifier
        {
            get
            {
                return this.countrySubdivisionTechnicalIdentifierField;
            }
            set
            {
                this.countrySubdivisionTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 1)]
        public string CountrySubdivisionCode
        {
            get
            {
                return this.countrySubdivisionCodeField;
            }
            set
            {
                this.countrySubdivisionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CountrySubdivisionType", Order = 2)]
        public CountrySubdivisionTypeCountrySubdivisionType CountrySubdivisionType1
        {
            get
            {
                return this.countrySubdivisionType1Field;
            }
            set
            {
                this.countrySubdivisionType1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string CountrySubdivisionMemberState
        {
            get
            {
                return this.countrySubdivisionMemberStateField;
            }
            set
            {
                this.countrySubdivisionMemberStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 4)]
        public string CountrySubdivisionToCountry
        {
            get
            {
                return this.countrySubdivisionToCountryField;
            }
            set
            {
                this.countrySubdivisionToCountryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public enum CountrySubdivisionTypeCountrySubdivisionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Autonomous Region")]
        AutonomousRegion,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Autonomous City")]
        AutonomousCity,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Autonomous Community")]
        AutonomousCommunity,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Council Area")]
        CouncilArea,

        /// <remarks/>
        Country,

        /// <remarks/>
        Department,

        /// <remarks/>
        District,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("District Council Area")]
        DistrictCouncilArea,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Federal Land")]
        FederalLand,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Local Council")]
        LocalCouncil,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Metropolitan Department")]
        MetropolitanDepartment,

        /// <remarks/>
        Municipality,

        /// <remarks/>
        Province,

        /// <remarks/>
        Region,

        /// <remarks/>
        State,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Unitary Authority")]
        UnitaryAuthority,

        /// <remarks/>
        County,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Administrative Region")]
        AdministrativeRegion,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Overseas Department")]
        OverseasDepartment,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Overseas Territory")]
        OverseasTerritory,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class CurrencyType : EntityType1
    {

        private string currencyTechnicalIdentifierField;

        private string currencyISO4217CodeField;

        private string currencyISO4217NumberField;

        private string[] currencyUsedInCountryField;

        private string[] currencyReplacedByField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string CurrencyTechnicalIdentifier
        {
            get
            {
                return this.currencyTechnicalIdentifierField;
            }
            set
            {
                this.currencyTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string CurrencyISO4217Code
        {
            get
            {
                return this.currencyISO4217CodeField;
            }
            set
            {
                this.currencyISO4217CodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string CurrencyISO4217Number
        {
            get
            {
                return this.currencyISO4217NumberField;
            }
            set
            {
                this.currencyISO4217NumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CurrencyUsedInCountry", DataType = "IDREF", Order = 3)]
        public string[] CurrencyUsedInCountry
        {
            get
            {
                return this.currencyUsedInCountryField;
            }
            set
            {
                this.currencyUsedInCountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CurrencyReplacedBy", DataType = "IDREF", Order = 4)]
        public string[] CurrencyReplacedBy
        {
            get
            {
                return this.currencyReplacedByField;
            }
            set
            {
                this.currencyReplacedByField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class CountryType : EntityType1
    {

        private string countryTechnicalIdentifierField;

        private string countryISO31662CodeField;

        private string countryISO31662NumberField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation1[] countryRemarkField;

        private bool countryUsedForNationalityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string CountryTechnicalIdentifier
        {
            get
            {
                return this.countryTechnicalIdentifierField;
            }
            set
            {
                this.countryTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CountryISO3166-2Code", Order = 1)]
        public string CountryISO31662Code
        {
            get
            {
                return this.countryISO31662CodeField;
            }
            set
            {
                this.countryISO31662CodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CountryISO3166-2Number", Order = 2)]
        public string CountryISO31662Number
        {
            get
            {
                return this.countryISO31662NumberField;
            }
            set
            {
                this.countryISO31662NumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation1[] CountryRemark
        {
            get
            {
                return this.countryRemarkField;
            }
            set
            {
                this.countryRemarkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public bool CountryUsedForNationality
        {
            get
            {
                return this.countryUsedForNationalityField;
            }
            set
            {
                this.countryUsedForNationalityField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class LanguageType : EntityType1
    {

        private string languageISO6391CodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LanguageISO639-1Code", Order = 0)]
        public string LanguageISO6391Code
        {
            get
            {
                return this.languageISO6391CodeField;
            }
            set
            {
                this.languageISO6391CodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/common-reference-tables-v1.0")]
    public partial class CentralAuthorityType : EntityType1
    {

        private string centralAuthorityTechnicalIdentifierField;

        private string centralAuthorityMemberStateCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string CentralAuthorityTechnicalIdentifier
        {
            get
            {
                return this.centralAuthorityTechnicalIdentifierField;
            }
            set
            {
                this.centralAuthorityTechnicalIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string CentralAuthorityMemberStateCode
        {
            get
            {
                return this.centralAuthorityMemberStateCodeField;
            }
            set
            {
                this.centralAuthorityMemberStateCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UpdateEventsStateWSDataInputType
    {

        private EventStateType eventStateField;

        private int[] eventIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public EventStateType EventState
        {
            get
            {
                return this.eventStateField;
            }
            set
            {
                this.eventStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EventIdentifier", Order = 1)]
        public int[] EventIdentifier
        {
            get
            {
                return this.eventIdentifierField;
            }
            set
            {
                this.eventIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EventStateType
    {

        /// <remarks/>
        AVAILABLE,

        /// <remarks/>
        ACKNOWLEDGED,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RecordedEventType
    {

        private int eventIdField;

        private string eventCodeField;

        private EventStateType eventStateField;

        private System.DateTime eventDateField;

        private string eventDetailsField;

        private EventSeverityType eventSeverityField;

        private EventTypeType eventTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int EventId
        {
            get
            {
                return this.eventIdField;
            }
            set
            {
                this.eventIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string EventCode
        {
            get
            {
                return this.eventCodeField;
            }
            set
            {
                this.eventCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public EventStateType EventState
        {
            get
            {
                return this.eventStateField;
            }
            set
            {
                this.eventStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public System.DateTime EventDate
        {
            get
            {
                return this.eventDateField;
            }
            set
            {
                this.eventDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string EventDetails
        {
            get
            {
                return this.eventDetailsField;
            }
            set
            {
                this.eventDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public EventSeverityType EventSeverity
        {
            get
            {
                return this.eventSeverityField;
            }
            set
            {
                this.eventSeverityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public EventTypeType EventType
        {
            get
            {
                return this.eventTypeField;
            }
            set
            {
                this.eventTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EventSeverityType
    {

        /// <remarks/>
        INFORMATION,

        /// <remarks/>
        WARNING,

        /// <remarks/>
        ERROR,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EventTypeType
    {

        /// <remarks/>
        SYSTEM,

        /// <remarks/>
        FUNCTIONAL,

        /// <remarks/>
        OPERATIONAL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetEventsWSOutputDataType
    {

        private RecordedEventType[] recordedEventField;

        private int totalEventsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RecordedEvent", Order = 0)]
        public RecordedEventType[] RecordedEvent
        {
            get
            {
                return this.recordedEventField;
            }
            set
            {
                this.recordedEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int TotalEvents
        {
            get
            {
                return this.totalEventsField;
            }
            set
            {
                this.totalEventsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class EventDescriptionType
    {

        private string languageCodeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetEventsWSDataInputType
    {

        private EventDescriptionType eventDescriptionField;

        private EventTypeType[] eventTypeField;

        private EventSeverityType[] eventSeverityField;

        private EventStateType eventStateField;

        private bool eventStateFieldSpecified;

        private StrictDateTimeType lastCheckField;

        private EventsSortByType eventsSortedByField;

        private bool eventsSortedByFieldSpecified;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public EventDescriptionType EventDescription
        {
            get
            {
                return this.eventDescriptionField;
            }
            set
            {
                this.eventDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EventType", Order = 1)]
        public EventTypeType[] EventType
        {
            get
            {
                return this.eventTypeField;
            }
            set
            {
                this.eventTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EventSeverity", Order = 2)]
        public EventSeverityType[] EventSeverity
        {
            get
            {
                return this.eventSeverityField;
            }
            set
            {
                this.eventSeverityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public EventStateType EventState
        {
            get
            {
                return this.eventStateField;
            }
            set
            {
                this.eventStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EventStateSpecified
        {
            get
            {
                return this.eventStateFieldSpecified;
            }
            set
            {
                this.eventStateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public StrictDateTimeType LastCheck
        {
            get
            {
                return this.lastCheckField;
            }
            set
            {
                this.lastCheckField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public EventsSortByType EventsSortedBy
        {
            get
            {
                return this.eventsSortedByField;
            }
            set
            {
                this.eventsSortedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EventsSortedBySpecified
        {
            get
            {
                return this.eventsSortedByFieldSpecified;
            }
            set
            {
                this.eventsSortedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 7)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class StrictDateTimeType
    {

        private string functionalErrorReferenceIdentifierField;

        private System.DateTime valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public System.DateTime Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum EventsSortByType
    {

        /// <remarks/>
        EventTypeAsc,

        /// <remarks/>
        EventTypeDesc,

        /// <remarks/>
        EventSeverityAsc,

        /// <remarks/>
        EventSeverityDesc,

        /// <remarks/>
        EventDateTimeAsc,

        /// <remarks/>
        EventDateTimeDesc,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ManageEventTypesConnectionsToUserRolesWSDataInputType
    {

        private string roleIdentifierField;

        private string[] eventCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string RoleIdentifier
        {
            get
            {
                return this.roleIdentifierField;
            }
            set
            {
                this.roleIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EventCode", Order = 1)]
        public string[] EventCode
        {
            get
            {
                return this.eventCodeField;
            }
            set
            {
                this.eventCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MailServerConnectionStatusWSOutputDataType
    {

        private bool mailServerConnectionStatusField;

        private bool mailSendingEnabledField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool MailServerConnectionStatus
        {
            get
            {
                return this.mailServerConnectionStatusField;
            }
            set
            {
                this.mailServerConnectionStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public bool MailSendingEnabled
        {
            get
            {
                return this.mailSendingEnabledField;
            }
            set
            {
                this.mailSendingEnabledField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetBuildNumberWSOutputDataType
    {

        private string buildNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string BuildNumber
        {
            get
            {
                return this.buildNumberField;
            }
            set
            {
                this.buildNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SetSystemMaintenanceModeWSInputDataType
    {

        private bool isMaintenanceModeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool IsMaintenanceMode
        {
            get
            {
                return this.isMaintenanceModeField;
            }
            set
            {
                this.isMaintenanceModeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ValidateConfigurationFilesWSOutputDataType
    {

        private string[] configurationFileListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ConfigurationFile", IsNullable = false)]
        public string[] ConfigurationFileList
        {
            get
            {
                return this.configurationFileListField;
            }
            set
            {
                this.configurationFileListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ValidateConfigurationFilesWSInputDataType
    {

        private byte[] configurationZipFileField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 0)]
        public byte[] ConfigurationZipFile
        {
            get
            {
                return this.configurationZipFileField;
            }
            set
            {
                this.configurationZipFileField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UploadConfigurationFilesWSOutputDataType
    {

        private string[] configurationFileListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ConfigurationFile", IsNullable = false)]
        public string[] ConfigurationFileList
        {
            get
            {
                return this.configurationFileListField;
            }
            set
            {
                this.configurationFileListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UploadConfigurationFilesWSInputDataType
    {

        private byte[] configurationZipFileField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 0)]
        public byte[] ConfigurationZipFile
        {
            get
            {
                return this.configurationZipFileField;
            }
            set
            {
                this.configurationZipFileField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ResetPasswordWsInput
    {

        private string loginUserNameField;

        private string loginCurrentPasswordField;

        private string loginNewPasswordField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string LoginUserName
        {
            get
            {
                return this.loginUserNameField;
            }
            set
            {
                this.loginUserNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string LoginCurrentPassword
        {
            get
            {
                return this.loginCurrentPasswordField;
            }
            set
            {
                this.loginCurrentPasswordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string LoginNewPassword
        {
            get
            {
                return this.loginNewPasswordField;
            }
            set
            {
                this.loginNewPasswordField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class PerformFunctionalValidationWSOutputDataType
    {

        private AbstractBusinessMessageType ecrisRiMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractBusinessMessageType EcrisRiMessage
        {
            get
            {
                return this.ecrisRiMessageField;
            }
            set
            {
                this.ecrisRiMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CancellationMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FunctionalErrorMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoResponseMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationResponseMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestResponseMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestDeadlineMessageType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestMessageType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractBusinessMessageType : AbstractMessageType
    {

        private string messageLastModifiedByUserField;

        private ContactPersonType messageContactPersonField;

        private FunctionalErrorsListType messageFunctionalErrorsField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] messageRemarksField;

        private string authoringLanguageField;

        private PersonType messagePersonField;

        private NistAttachmentType messageNistAttachmentField;

        private ConvictionType notificationMessageConvictionField;

        private UpdateConvictionReferenceType notificationMessageUpdatedConvictionReferenceField;

        private ConvictionToConvictionsRelationshipType notificationMessageOtherAffectedConvictionField;

        private NotificationResponseTypeExternalReferenceType notificationResponseMessageNotificationResponseTypeReferenceField;

        private RequestAdditionalInfoTypeExternalReferenceType requestAdditionalInfoResponseMessageRequestAdditionalInfoResponseTypeReferenceField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] requestAdditionalInfoMessageRequestPurposeField;

        private RequestPurposeExternalReferenceType requestMessageRequestPurposeCategoryReferenceField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] requestMessageRequestPurposeField;

        private OffenceExternalReferenceType[] requestMessageAccusationOffenceCategoryReferenceField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] requestMessageAccusationField;

        private RequestingAuthorityType requestMessageRequestingAuthorityField;

        private RestrictedStringType50Chars requestMessageCaseReferenceNumberField;

        private YesNoUnknownStringEnumerationType requestMessageConcernedPersonConsentField;

        private YesNoUnknownStringEnumerationType requestMessageUrgencyField;

        private ConvictionType[] requestResponseMessageConvictionField;

        private MemberStateCodeType[] requestResponseMessageOtherMemberStateField;

        private RequestResponseTypeExternalReferenceType requestResponseMessageRequestResponseTypeReferenceField;

        private FunctionalErrorType[] messageFunctionalErrorField;

        private AdditionalInformationRequestedEnumeration[] requestAdditionalInfoMessageAdditionalInformationRequestedField;

        private NistAttachmentType requestAdditionalInfoMessageBinaryIDRequestedField;

        private StrictDateType requestDeadlineMessageDeadlineField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public ContactPersonType MessageContactPerson
        {
            get
            {
                return this.messageContactPersonField;
            }
            set
            {
                this.messageContactPersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public FunctionalErrorsListType MessageFunctionalErrors
        {
            get
            {
                return this.messageFunctionalErrorsField;
            }
            set
            {
                this.messageFunctionalErrorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] MessageRemarks
        {
            get
            {
                return this.messageRemarksField;
            }
            set
            {
                this.messageRemarksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public PersonType MessagePerson
        {
            get
            {
                return this.messagePersonField;
            }
            set
            {
                this.messagePersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public NistAttachmentType MessageNistAttachment
        {
            get
            {
                return this.messageNistAttachmentField;
            }
            set
            {
                this.messageNistAttachmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public ConvictionType NotificationMessageConviction
        {
            get
            {
                return this.notificationMessageConvictionField;
            }
            set
            {
                this.notificationMessageConvictionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public UpdateConvictionReferenceType NotificationMessageUpdatedConvictionReference
        {
            get
            {
                return this.notificationMessageUpdatedConvictionReferenceField;
            }
            set
            {
                this.notificationMessageUpdatedConvictionReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public ConvictionToConvictionsRelationshipType NotificationMessageOtherAffectedConviction
        {
            get
            {
                return this.notificationMessageOtherAffectedConvictionField;
            }
            set
            {
                this.notificationMessageOtherAffectedConvictionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public NotificationResponseTypeExternalReferenceType NotificationResponseMessageNotificationResponseTypeReference
        {
            get
            {
                return this.notificationResponseMessageNotificationResponseTypeReferenceField;
            }
            set
            {
                this.notificationResponseMessageNotificationResponseTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public RequestAdditionalInfoTypeExternalReferenceType RequestAdditionalInfoResponseMessageRequestAdditionalInfoResponseTypeReference
        {
            get
            {
                return this.requestAdditionalInfoResponseMessageRequestAdditionalInfoResponseTypeReferenceField;
            }
            set
            {
                this.requestAdditionalInfoResponseMessageRequestAdditionalInfoResponseTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 12)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] RequestAdditionalInfoMessageRequestPurpose
        {
            get
            {
                return this.requestAdditionalInfoMessageRequestPurposeField;
            }
            set
            {
                this.requestAdditionalInfoMessageRequestPurposeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public RequestPurposeExternalReferenceType RequestMessageRequestPurposeCategoryReference
        {
            get
            {
                return this.requestMessageRequestPurposeCategoryReferenceField;
            }
            set
            {
                this.requestMessageRequestPurposeCategoryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 14)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] RequestMessageRequestPurpose
        {
            get
            {
                return this.requestMessageRequestPurposeField;
            }
            set
            {
                this.requestMessageRequestPurposeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestMessageAccusationOffenceCategoryReference", Order = 15)]
        public OffenceExternalReferenceType[] RequestMessageAccusationOffenceCategoryReference
        {
            get
            {
                return this.requestMessageAccusationOffenceCategoryReferenceField;
            }
            set
            {
                this.requestMessageAccusationOffenceCategoryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 16)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] RequestMessageAccusation
        {
            get
            {
                return this.requestMessageAccusationField;
            }
            set
            {
                this.requestMessageAccusationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public RequestingAuthorityType RequestMessageRequestingAuthority
        {
            get
            {
                return this.requestMessageRequestingAuthorityField;
            }
            set
            {
                this.requestMessageRequestingAuthorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public RestrictedStringType50Chars RequestMessageCaseReferenceNumber
        {
            get
            {
                return this.requestMessageCaseReferenceNumberField;
            }
            set
            {
                this.requestMessageCaseReferenceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public YesNoUnknownStringEnumerationType RequestMessageConcernedPersonConsent
        {
            get
            {
                return this.requestMessageConcernedPersonConsentField;
            }
            set
            {
                this.requestMessageConcernedPersonConsentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public YesNoUnknownStringEnumerationType RequestMessageUrgency
        {
            get
            {
                return this.requestMessageUrgencyField;
            }
            set
            {
                this.requestMessageUrgencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestResponseMessageConviction", Order = 21)]
        public ConvictionType[] RequestResponseMessageConviction
        {
            get
            {
                return this.requestResponseMessageConvictionField;
            }
            set
            {
                this.requestResponseMessageConvictionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestResponseMessageOtherMemberState", Order = 22)]
        public MemberStateCodeType[] RequestResponseMessageOtherMemberState
        {
            get
            {
                return this.requestResponseMessageOtherMemberStateField;
            }
            set
            {
                this.requestResponseMessageOtherMemberStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public RequestResponseTypeExternalReferenceType RequestResponseMessageRequestResponseTypeReference
        {
            get
            {
                return this.requestResponseMessageRequestResponseTypeReferenceField;
            }
            set
            {
                this.requestResponseMessageRequestResponseTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MessageFunctionalError", Order = 24)]
        public FunctionalErrorType[] MessageFunctionalError
        {
            get
            {
                return this.messageFunctionalErrorField;
            }
            set
            {
                this.messageFunctionalErrorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestAdditionalInfoMessageAdditionalInformationRequested", Order = 25)]
        public AdditionalInformationRequestedEnumeration[] RequestAdditionalInfoMessageAdditionalInformationRequested
        {
            get
            {
                return this.requestAdditionalInfoMessageAdditionalInformationRequestedField;
            }
            set
            {
                this.requestAdditionalInfoMessageAdditionalInformationRequestedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
        public NistAttachmentType RequestAdditionalInfoMessageBinaryIDRequested
        {
            get
            {
                return this.requestAdditionalInfoMessageBinaryIDRequestedField;
            }
            set
            {
                this.requestAdditionalInfoMessageBinaryIDRequestedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
        public StrictDateType RequestDeadlineMessageDeadline
        {
            get
            {
                return this.requestDeadlineMessageDeadlineField;
            }
            set
            {
                this.requestDeadlineMessageDeadlineField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class ContactPersonType
    {

        private NameTextType forenameField;

        private NameTextType surnameField;

        private NameTextType secondSurnameField;

        private string contactPersonPhoneNumberField;

        private string contactPersonFaxNumberField;

        private string contactPersonEmailField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public NameTextType Forename
        {
            get
            {
                return this.forenameField;
            }
            set
            {
                this.forenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public NameTextType Surname
        {
            get
            {
                return this.surnameField;
            }
            set
            {
                this.surnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public NameTextType SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string ContactPersonPhoneNumber
        {
            get
            {
                return this.contactPersonPhoneNumberField;
            }
            set
            {
                this.contactPersonPhoneNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string ContactPersonFaxNumber
        {
            get
            {
                return this.contactPersonFaxNumberField;
            }
            set
            {
                this.contactPersonFaxNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string ContactPersonEmail
        {
            get
            {
                return this.contactPersonEmailField;
            }
            set
            {
                this.contactPersonEmailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class NameTextType
    {

        private string functionalErrorReferenceIdentifierField;

        private string languageCodeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class FunctionalErrorsListType
    {

        private System.DateTime validationTimestampField;

        private bool validationTimestampFieldSpecified;

        private FunctionalErrorsListTypeFunctionalError[] functionalErrorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public System.DateTime ValidationTimestamp
        {
            get
            {
                return this.validationTimestampField;
            }
            set
            {
                this.validationTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValidationTimestampSpecified
        {
            get
            {
                return this.validationTimestampFieldSpecified;
            }
            set
            {
                this.validationTimestampFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FunctionalError", Order = 1)]
        public FunctionalErrorsListTypeFunctionalError[] FunctionalError
        {
            get
            {
                return this.functionalErrorField;
            }
            set
            {
                this.functionalErrorField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class FunctionalErrorsListTypeFunctionalError
    {

        private string functionalErrorCodeField;

        private string[] functionalErrorReferencedElementField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string FunctionalErrorCode
        {
            get
            {
                return this.functionalErrorCodeField;
            }
            set
            {
                this.functionalErrorCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FunctionalErrorReferencedElement", DataType = "IDREF", Order = 1)]
        public string[] FunctionalErrorReferencedElement
        {
            get
            {
                return this.functionalErrorReferencedElementField;
            }
            set
            {
                this.functionalErrorReferencedElementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation
    {

        private string functionalErrorReferenceIdentifierField;

        private string languageCodeField;

        private bool translatedField;

        private bool translatedFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool translated
        {
            get
            {
                return this.translatedField;
            }
            set
            {
                this.translatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool translatedSpecified
        {
            get
            {
                return this.translatedFieldSpecified;
            }
            set
            {
                this.translatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class PersonType : AbstractPersonType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MessageShortViewPersonType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PersonType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PersonAliasType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractPersonType
    {

        private PersonNameType personNameField;

        private int personSexField;

        private bool personSexFieldSpecified;

        private DateType personBirthDateField;

        private AbstractPlaceType personBirthPlaceField;

        private CountryExternalReferenceType[] personNationalityReferenceField;

        private NameTextType[] personFormerForenameField;

        private NameTextType[] personFormerSurnameField;

        private NameTextType[] personFormerSecondSurnameField;

        private NameTextType[] personFatherForenameField;

        private NameTextType[] personFatherSurnameField;

        private NameTextType[] personFatherSecondSurnameField;

        private NameTextType[] personMotherForenameField;

        private NameTextType[] personMotherSurnameField;

        private NameTextType[] personMotherSecondSurnameField;

        private RestrictedStringType50Chars personIdentityNumberField;

        private IdentificationDocumentType[] personIdentificationDocumentField;

        private PersonAddressType[] personAddressField;

        private PersonAliasType[] personAliasField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] personRemarksField;

        private string functionalErrorReferenceIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public PersonNameType PersonName
        {
            get
            {
                return this.personNameField;
            }
            set
            {
                this.personNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int PersonSex
        {
            get
            {
                return this.personSexField;
            }
            set
            {
                this.personSexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PersonSexSpecified
        {
            get
            {
                return this.personSexFieldSpecified;
            }
            set
            {
                this.personSexFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public DateType PersonBirthDate
        {
            get
            {
                return this.personBirthDateField;
            }
            set
            {
                this.personBirthDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public AbstractPlaceType PersonBirthPlace
        {
            get
            {
                return this.personBirthPlaceField;
            }
            set
            {
                this.personBirthPlaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonNationalityReference", Order = 4)]
        public CountryExternalReferenceType[] PersonNationalityReference
        {
            get
            {
                return this.personNationalityReferenceField;
            }
            set
            {
                this.personNationalityReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonFormerForename", Order = 5)]
        public NameTextType[] PersonFormerForename
        {
            get
            {
                return this.personFormerForenameField;
            }
            set
            {
                this.personFormerForenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonFormerSurname", Order = 6)]
        public NameTextType[] PersonFormerSurname
        {
            get
            {
                return this.personFormerSurnameField;
            }
            set
            {
                this.personFormerSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonFormerSecondSurname", Order = 7)]
        public NameTextType[] PersonFormerSecondSurname
        {
            get
            {
                return this.personFormerSecondSurnameField;
            }
            set
            {
                this.personFormerSecondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonFatherForename", Order = 8)]
        public NameTextType[] PersonFatherForename
        {
            get
            {
                return this.personFatherForenameField;
            }
            set
            {
                this.personFatherForenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonFatherSurname", Order = 9)]
        public NameTextType[] PersonFatherSurname
        {
            get
            {
                return this.personFatherSurnameField;
            }
            set
            {
                this.personFatherSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonFatherSecondSurname", Order = 10)]
        public NameTextType[] PersonFatherSecondSurname
        {
            get
            {
                return this.personFatherSecondSurnameField;
            }
            set
            {
                this.personFatherSecondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonMotherForename", Order = 11)]
        public NameTextType[] PersonMotherForename
        {
            get
            {
                return this.personMotherForenameField;
            }
            set
            {
                this.personMotherForenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonMotherSurname", Order = 12)]
        public NameTextType[] PersonMotherSurname
        {
            get
            {
                return this.personMotherSurnameField;
            }
            set
            {
                this.personMotherSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonMotherSecondSurname", Order = 13)]
        public NameTextType[] PersonMotherSecondSurname
        {
            get
            {
                return this.personMotherSecondSurnameField;
            }
            set
            {
                this.personMotherSecondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public RestrictedStringType50Chars PersonIdentityNumber
        {
            get
            {
                return this.personIdentityNumberField;
            }
            set
            {
                this.personIdentityNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonIdentificationDocument", Order = 15)]
        public IdentificationDocumentType[] PersonIdentificationDocument
        {
            get
            {
                return this.personIdentificationDocumentField;
            }
            set
            {
                this.personIdentificationDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonAddress", Order = 16)]
        public PersonAddressType[] PersonAddress
        {
            get
            {
                return this.personAddressField;
            }
            set
            {
                this.personAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PersonAlias", Order = 17)]
        public PersonAliasType[] PersonAlias
        {
            get
            {
                return this.personAliasField;
            }
            set
            {
                this.personAliasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 18)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] PersonRemarks
        {
            get
            {
                return this.personRemarksField;
            }
            set
            {
                this.personRemarksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class PersonNameType : AbstractNameType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PersonNameType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractNameType
    {

        private NameTextType[] forenameField;

        private NameTextType[] surnameField;

        private NameTextType[] secondSurnameField;

        private FullNameTextType[] fullNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Forename", Order = 0)]
        public NameTextType[] Forename
        {
            get
            {
                return this.forenameField;
            }
            set
            {
                this.forenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Surname", Order = 1)]
        public NameTextType[] Surname
        {
            get
            {
                return this.surnameField;
            }
            set
            {
                this.surnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SecondSurname", Order = 2)]
        public NameTextType[] SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FullName", Order = 3)]
        public FullNameTextType[] FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class FullNameTextType
    {

        private string functionalErrorReferenceIdentifierField;

        private string languageCodeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class DateType
    {

        private string dateYearField;

        private MonthDayType dateMonthDayField;

        private string functionalErrorReferenceIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear", Order = 0)]
        public string DateYear
        {
            get
            {
                return this.dateYearField;
            }
            set
            {
                this.dateYearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MonthDayType DateMonthDay
        {
            get
            {
                return this.dateMonthDayField;
            }
            set
            {
                this.dateMonthDayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class MonthDayType
    {

        private string dateMonthField;

        private string dateDayField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gMonth", Order = 0)]
        public string DateMonth
        {
            get
            {
                return this.dateMonthField;
            }
            set
            {
                this.dateMonthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gDay", Order = 1)]
        public string DateDay
        {
            get
            {
                return this.dateDayField;
            }
            set
            {
                this.dateDayField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AliasBirthPlaceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PlaceType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractPlaceType
    {

        private CountryExternalReferenceType placeCountryReferenceField;

        private CountrySubdivisionExternalReferenceType placeCountrySubdivisionReferenceField;

        private CityExternalReferenceType placeTownReferenceField;

        private MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[] placeTownNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public CountryExternalReferenceType PlaceCountryReference
        {
            get
            {
                return this.placeCountryReferenceField;
            }
            set
            {
                this.placeCountryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public CountrySubdivisionExternalReferenceType PlaceCountrySubdivisionReference
        {
            get
            {
                return this.placeCountrySubdivisionReferenceField;
            }
            set
            {
                this.placeCountrySubdivisionReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
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
        [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[] PlaceTownName
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class CountryExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NJRConvictionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConvictionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationResponseTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestResponseTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestingAuthorityTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DecisionChangeTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(IdentificationDocumentCategoryExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestPurposeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionNatureExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionTypeOfSuspensionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionAlternativeTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfCompletionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfParticipationExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CityExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountrySubdivisionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CurrencyExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountryExternalReferenceType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class NonBindingExternalReferenceType : StringType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(YesNoUnknownStringEnumerationType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NonBindingExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NJRConvictionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConvictionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestAdditionalInfoTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotificationResponseTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestResponseTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestingAuthorityTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DecisionChangeTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(IdentificationDocumentCategoryExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestPurposeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionNatureExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionTypeOfSuspensionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionAlternativeTypeExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfCompletionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceLevelOfParticipationExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CityExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountrySubdivisionExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CurrencyExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountryExternalReferenceType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BusinessStringType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RestrictedStringType50Chars))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class StringType
    {

        private string functionalErrorReferenceIdentifierField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class YesNoUnknownStringEnumerationType : StringType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RestrictedStringType50Chars))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class BusinessStringType : StringType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class RestrictedStringType50Chars : BusinessStringType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class NJRConvictionExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class ConvictionExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class RequestAdditionalInfoTypeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class NotificationResponseTypeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class RequestResponseTypeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class RequestingAuthorityTypeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class DecisionChangeTypeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class IdentificationDocumentCategoryExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class RequestPurposeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class SanctionNatureExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class SanctionTypeOfSuspensionExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class SanctionAlternativeTypeExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class SanctionExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class OffenceLevelOfCompletionExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class OffenceLevelOfParticipationExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class OffenceExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class CityExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class CountrySubdivisionExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public partial class CurrencyExternalReferenceType : NonBindingExternalReferenceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class MultilingualTextType200CharsMultilingualTextLinguisticRepresentation
    {

        private string functionalErrorReferenceIdentifierField;

        private string languageCodeField;

        private bool translatedField;

        private bool translatedFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool translated
        {
            get
            {
                return this.translatedField;
            }
            set
            {
                this.translatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool translatedSpecified
        {
            get
            {
                return this.translatedFieldSpecified;
            }
            set
            {
                this.translatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class AliasBirthPlaceType : AbstractPlaceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class PlaceType : AbstractPlaceType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class IdentificationDocumentType
    {

        private IdentificationDocumentCategoryExternalReferenceType identificationDocumentCategoryReferenceField;

        private MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[] identificationDocumentType1Field;

        private string identificationDocumentNumberField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] identificationDocumentIssuingAuthorityField;

        private DateType identificationDocumentIssuingDateField;

        private DateType identificationDocumentValidUntilField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public IdentificationDocumentCategoryExternalReferenceType IdentificationDocumentCategoryReference
        {
            get
            {
                return this.identificationDocumentCategoryReferenceField;
            }
            set
            {
                this.identificationDocumentCategoryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute("IdentificationDocumentType", Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[] IdentificationDocumentType1
        {
            get
            {
                return this.identificationDocumentType1Field;
            }
            set
            {
                this.identificationDocumentType1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string IdentificationDocumentNumber
        {
            get
            {
                return this.identificationDocumentNumberField;
            }
            set
            {
                this.identificationDocumentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] IdentificationDocumentIssuingAuthority
        {
            get
            {
                return this.identificationDocumentIssuingAuthorityField;
            }
            set
            {
                this.identificationDocumentIssuingAuthorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public DateType IdentificationDocumentIssuingDate
        {
            get
            {
                return this.identificationDocumentIssuingDateField;
            }
            set
            {
                this.identificationDocumentIssuingDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public DateType IdentificationDocumentValidUntil
        {
            get
            {
                return this.identificationDocumentValidUntilField;
            }
            set
            {
                this.identificationDocumentValidUntilField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class MultilingualTextType50CharsMultilingualTextLinguisticRepresentation
    {

        private string functionalErrorReferenceIdentifierField;

        private string languageCodeField;

        private bool translatedField;

        private bool translatedFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool translated
        {
            get
            {
                return this.translatedField;
            }
            set
            {
                this.translatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool translatedSpecified
        {
            get
            {
                return this.translatedFieldSpecified;
            }
            set
            {
                this.translatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class MultilingualTextType400CharsMultilingualTextLinguisticRepresentation
    {

        private string functionalErrorReferenceIdentifierField;

        private string languageCodeField;

        private bool translatedField;

        private bool translatedFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool translated
        {
            get
            {
                return this.translatedField;
            }
            set
            {
                this.translatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool translatedSpecified
        {
            get
            {
                return this.translatedFieldSpecified;
            }
            set
            {
                this.translatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class PersonAddressType
    {

        private PlaceType addressPlaceField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] addressStreetField;

        private MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[] addressHouseNumberField;

        private MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[] addressPostCodeField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] addressFullField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public PlaceType AddressPlace
        {
            get
            {
                return this.addressPlaceField;
            }
            set
            {
                this.addressPlaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] AddressStreet
        {
            get
            {
                return this.addressStreetField;
            }
            set
            {
                this.addressStreetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[] AddressHouseNumber
        {
            get
            {
                return this.addressHouseNumberField;
            }
            set
            {
                this.addressHouseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[] AddressPostCode
        {
            get
            {
                return this.addressPostCodeField;
            }
            set
            {
                this.addressPostCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] AddressFull
        {
            get
            {
                return this.addressFullField;
            }
            set
            {
                this.addressFullField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class PersonAliasType : AbstractPersonType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class MessageShortViewPersonType : AbstractPersonType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NistBinaryAttachmentType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class NistAttachmentType
    {

        private string nistAttachmentIDField;

        private string nistAttachmentFileNameField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] nistAttachmentTitleField;

        private string nistAttachmentFileSizeField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] nistAttachmentCommentField;

        private RestrictedStringType50Chars nistAttachmentMD5HashField;

        private string functionalErrorReferenceIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string NistAttachmentID
        {
            get
            {
                return this.nistAttachmentIDField;
            }
            set
            {
                this.nistAttachmentIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string NistAttachmentFileName
        {
            get
            {
                return this.nistAttachmentFileNameField;
            }
            set
            {
                this.nistAttachmentFileNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] NistAttachmentTitle
        {
            get
            {
                return this.nistAttachmentTitleField;
            }
            set
            {
                this.nistAttachmentTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 3)]
        public string NistAttachmentFileSize
        {
            get
            {
                return this.nistAttachmentFileSizeField;
            }
            set
            {
                this.nistAttachmentFileSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] NistAttachmentComment
        {
            get
            {
                return this.nistAttachmentCommentField;
            }
            set
            {
                this.nistAttachmentCommentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public RestrictedStringType50Chars NistAttachmentMD5Hash
        {
            get
            {
                return this.nistAttachmentMD5HashField;
            }
            set
            {
                this.nistAttachmentMD5HashField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class NistBinaryAttachmentType : NistAttachmentType
    {

        private byte[] nistBinaryAttachmentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 0)]
        public byte[] NistBinaryAttachment
        {
            get
            {
                return this.nistBinaryAttachmentField;
            }
            set
            {
                this.nistBinaryAttachmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class ConvictionType
    {

        private string convictionIDField;

        private CountryExternalReferenceType convictionConvictingCountryReferenceField;

        private RestrictedStringType50Chars convictionFileNumberField;

        private StrictDateType convictionDecisionDateField;

        private StrictDateType convictionDecisionFinalDateField;

        private DecidingAuthorityType convictionDecidingAuthorityField;

        private YesNoUnknownStringEnumerationType convictionNonCriminalRulingField;

        private StrictDateType convictionRetentionPeriodEndDateField;

        private YesNoUnknownStringEnumerationType convictionIsTransmittableField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] convictionRemarksField;

        private DecisionType[] convictionDecisionField;

        private OffenceType[] convictionOffenceField;

        private SanctionType[] convictionSanctionField;

        private AbstractRelationshipType[] convictionRelationshipField;
        [XmlIgnoreAttribute]
        public string? FbbcId { get; set; }
        [XmlIgnoreAttribute]
        public string? BuletinId { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string ConvictionID
        {
            get
            {
                return this.convictionIDField;
            }
            set
            {
                this.convictionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public CountryExternalReferenceType ConvictionConvictingCountryReference
        {
            get
            {
                return this.convictionConvictingCountryReferenceField;
            }
            set
            {
                this.convictionConvictingCountryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public RestrictedStringType50Chars ConvictionFileNumber
        {
            get
            {
                return this.convictionFileNumberField;
            }
            set
            {
                this.convictionFileNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public StrictDateType ConvictionDecisionDate
        {
            get
            {
                return this.convictionDecisionDateField;
            }
            set
            {
                this.convictionDecisionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public StrictDateType ConvictionDecisionFinalDate
        {
            get
            {
                return this.convictionDecisionFinalDateField;
            }
            set
            {
                this.convictionDecisionFinalDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public DecidingAuthorityType ConvictionDecidingAuthority
        {
            get
            {
                return this.convictionDecidingAuthorityField;
            }
            set
            {
                this.convictionDecidingAuthorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public YesNoUnknownStringEnumerationType ConvictionNonCriminalRuling
        {
            get
            {
                return this.convictionNonCriminalRulingField;
            }
            set
            {
                this.convictionNonCriminalRulingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public StrictDateType ConvictionRetentionPeriodEndDate
        {
            get
            {
                return this.convictionRetentionPeriodEndDateField;
            }
            set
            {
                this.convictionRetentionPeriodEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public YesNoUnknownStringEnumerationType ConvictionIsTransmittable
        {
            get
            {
                return this.convictionIsTransmittableField;
            }
            set
            {
                this.convictionIsTransmittableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 9)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] ConvictionRemarks
        {
            get
            {
                return this.convictionRemarksField;
            }
            set
            {
                this.convictionRemarksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ConvictionDecision", Order = 10)]
        public DecisionType[] ConvictionDecision
        {
            get
            {
                return this.convictionDecisionField;
            }
            set
            {
                this.convictionDecisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ConvictionOffence", Order = 11)]
        public OffenceType[] ConvictionOffence
        {
            get
            {
                return this.convictionOffenceField;
            }
            set
            {
                this.convictionOffenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ConvictionSanction", Order = 12)]
        public SanctionType[] ConvictionSanction
        {
            get
            {
                return this.convictionSanctionField;
            }
            set
            {
                this.convictionSanctionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ConvictionRelationship", Order = 13)]
        public AbstractRelationshipType[] ConvictionRelationship
        {
            get
            {
                return this.convictionRelationshipField;
            }
            set
            {
                this.convictionRelationshipField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class DecidingAuthorityType
    {

        private RestrictedStringType50Chars decidingAuthorityCodeField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] decidingAuthorityNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RestrictedStringType50Chars DecidingAuthorityCode
        {
            get
            {
                return this.decidingAuthorityCodeField;
            }
            set
            {
                this.decidingAuthorityCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] DecidingAuthorityName
        {
            get
            {
                return this.decidingAuthorityNameField;
            }
            set
            {
                this.decidingAuthorityNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class DecisionType
    {

        private string decisionIDField;

        private DecisionChangeTypeExternalReferenceType[] decisionChangeTypeReferenceField;

        private StrictDateType decisionDateField;

        private StrictDateType decisionFinalDateField;

        private DecidingAuthorityType decisionDecidingAuthorityField;

        private YesNoUnknownStringEnumerationType decisionDeleteConvictionFromRegisterField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] decisionRemarksField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string DecisionID
        {
            get
            {
                return this.decisionIDField;
            }
            set
            {
                this.decisionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DecisionChangeTypeReference", Order = 1)]
        public DecisionChangeTypeExternalReferenceType[] DecisionChangeTypeReference
        {
            get
            {
                return this.decisionChangeTypeReferenceField;
            }
            set
            {
                this.decisionChangeTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public StrictDateType DecisionDate
        {
            get
            {
                return this.decisionDateField;
            }
            set
            {
                this.decisionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public StrictDateType DecisionFinalDate
        {
            get
            {
                return this.decisionFinalDateField;
            }
            set
            {
                this.decisionFinalDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public DecidingAuthorityType DecisionDecidingAuthority
        {
            get
            {
                return this.decisionDecidingAuthorityField;
            }
            set
            {
                this.decisionDecidingAuthorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public YesNoUnknownStringEnumerationType DecisionDeleteConvictionFromRegister
        {
            get
            {
                return this.decisionDeleteConvictionFromRegisterField;
            }
            set
            {
                this.decisionDeleteConvictionFromRegisterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 6)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] DecisionRemarks
        {
            get
            {
                return this.decisionRemarksField;
            }
            set
            {
                this.decisionRemarksField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class OffenceType : AbstractNationalCategoriesSupportingEntityType
    {

        private string offenceIDField;

        private OffenceExternalReferenceType offenceCommonCategoryReferenceField;

        private string offenceApplicableLegalProvisionsField;

        private DateType offenceStartDateField;

        private DateType offenceEndDateField;

        private PlaceType offencePlaceField;

        private string offenceNumberOfOccurrencesField;

        private YesNoUnknownStringEnumerationType offenceIsContinuousField;

        private OffenceLevelOfCompletionExternalReferenceType offenceLevelOfCompletionReferenceField;

        private OffenceLevelOfParticipationExternalReferenceType offenceLevelOfParticipationReferenceField;

        private YesNoUnknownStringEnumerationType offenceResponsibilityExemptionField;

        private YesNoUnknownStringEnumerationType offenceRecidivismField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string OffenceID
        {
            get
            {
                return this.offenceIDField;
            }
            set
            {
                this.offenceIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public OffenceExternalReferenceType OffenceCommonCategoryReference
        {
            get
            {
                return this.offenceCommonCategoryReferenceField;
            }
            set
            {
                this.offenceCommonCategoryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string OffenceApplicableLegalProvisions
        {
            get
            {
                return this.offenceApplicableLegalProvisionsField;
            }
            set
            {
                this.offenceApplicableLegalProvisionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public DateType OffenceStartDate
        {
            get
            {
                return this.offenceStartDateField;
            }
            set
            {
                this.offenceStartDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public DateType OffenceEndDate
        {
            get
            {
                return this.offenceEndDateField;
            }
            set
            {
                this.offenceEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public PlaceType OffencePlace
        {
            get
            {
                return this.offencePlaceField;
            }
            set
            {
                this.offencePlaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 6)]
        public string OffenceNumberOfOccurrences
        {
            get
            {
                return this.offenceNumberOfOccurrencesField;
            }
            set
            {
                this.offenceNumberOfOccurrencesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public YesNoUnknownStringEnumerationType OffenceIsContinuous
        {
            get
            {
                return this.offenceIsContinuousField;
            }
            set
            {
                this.offenceIsContinuousField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public OffenceLevelOfCompletionExternalReferenceType OffenceLevelOfCompletionReference
        {
            get
            {
                return this.offenceLevelOfCompletionReferenceField;
            }
            set
            {
                this.offenceLevelOfCompletionReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public OffenceLevelOfParticipationExternalReferenceType OffenceLevelOfParticipationReference
        {
            get
            {
                return this.offenceLevelOfParticipationReferenceField;
            }
            set
            {
                this.offenceLevelOfParticipationReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public YesNoUnknownStringEnumerationType OffenceResponsibilityExemption
        {
            get
            {
                return this.offenceResponsibilityExemptionField;
            }
            set
            {
                this.offenceResponsibilityExemptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public YesNoUnknownStringEnumerationType OffenceRecidivism
        {
            get
            {
                return this.offenceRecidivismField;
            }
            set
            {
                this.offenceRecidivismField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OffenceType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractNationalCategoriesSupportingEntityType
    {

        private string nationalCategoryCodeField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] nationalCategoryTitleField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] remarksField;

        private string functionalErrorReferenceIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string NationalCategoryCode
        {
            get
            {
                return this.nationalCategoryCodeField;
            }
            set
            {
                this.nationalCategoryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] NationalCategoryTitle
        {
            get
            {
                return this.nationalCategoryTitleField;
            }
            set
            {
                this.nationalCategoryTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionType : AbstractNationalCategoriesSupportingEntityType
    {

        private string sanctionIDField;

        private SanctionNatureExternalReferenceType sanctionTypeReferenceField;

        private SanctionExternalReferenceType sanctionCommonCategoryReferenceField;

        private SanctionAlternativeTypeExternalReferenceType sanctionAlternativeTypeReferenceField;

        private string sanctionMultiplierField;

        private YesNoUnknownStringEnumerationType sanctionIsSpecificToMinorField;

        private SanctionSentencedPeriodType sanctionSentencedPeriodField;

        private SanctionPeriodType sanctionExecutionPeriodField;

        private RestrictedPositiveIntegerWithErrorsType sanctionNumberOfFinesField;

        private PositiveDecimalType sanctionAmountOfIndividualFineField;

        private CurrencyExternalReferenceType sanctionCurrencyOfFineReferenceField;

        private SanctionSuspensionType sanctionSuspensionField;

        private SanctionInterruptionType[] sanctionInterruptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string SanctionID
        {
            get
            {
                return this.sanctionIDField;
            }
            set
            {
                this.sanctionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public SanctionNatureExternalReferenceType SanctionTypeReference
        {
            get
            {
                return this.sanctionTypeReferenceField;
            }
            set
            {
                this.sanctionTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public SanctionExternalReferenceType SanctionCommonCategoryReference
        {
            get
            {
                return this.sanctionCommonCategoryReferenceField;
            }
            set
            {
                this.sanctionCommonCategoryReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public SanctionAlternativeTypeExternalReferenceType SanctionAlternativeTypeReference
        {
            get
            {
                return this.sanctionAlternativeTypeReferenceField;
            }
            set
            {
                this.sanctionAlternativeTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 4)]
        public string SanctionMultiplier
        {
            get
            {
                return this.sanctionMultiplierField;
            }
            set
            {
                this.sanctionMultiplierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public YesNoUnknownStringEnumerationType SanctionIsSpecificToMinor
        {
            get
            {
                return this.sanctionIsSpecificToMinorField;
            }
            set
            {
                this.sanctionIsSpecificToMinorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public SanctionSentencedPeriodType SanctionSentencedPeriod
        {
            get
            {
                return this.sanctionSentencedPeriodField;
            }
            set
            {
                this.sanctionSentencedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public SanctionPeriodType SanctionExecutionPeriod
        {
            get
            {
                return this.sanctionExecutionPeriodField;
            }
            set
            {
                this.sanctionExecutionPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public RestrictedPositiveIntegerWithErrorsType SanctionNumberOfFines
        {
            get
            {
                return this.sanctionNumberOfFinesField;
            }
            set
            {
                this.sanctionNumberOfFinesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public PositiveDecimalType SanctionAmountOfIndividualFine
        {
            get
            {
                return this.sanctionAmountOfIndividualFineField;
            }
            set
            {
                this.sanctionAmountOfIndividualFineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public CurrencyExternalReferenceType SanctionCurrencyOfFineReference
        {
            get
            {
                return this.sanctionCurrencyOfFineReferenceField;
            }
            set
            {
                this.sanctionCurrencyOfFineReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public SanctionSuspensionType SanctionSuspension
        {
            get
            {
                return this.sanctionSuspensionField;
            }
            set
            {
                this.sanctionSuspensionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SanctionInterruption", Order = 12)]
        public SanctionInterruptionType[] SanctionInterruption
        {
            get
            {
                return this.sanctionInterruptionField;
            }
            set
            {
                this.sanctionInterruptionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionSentencedPeriodType : AbstractPeriodType
    {

        private YesNoUnknownStringEnumerationType durationExactField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public YesNoUnknownStringEnumerationType DurationExact
        {
            get
            {
                return this.durationExactField;
            }
            set
            {
                this.durationExactField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionInterruptionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionSuspensionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionSentencedPeriodType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionPeriodType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public abstract partial class AbstractPeriodType
    {

        private StrictDateType periodStartDateField;

        private string periodDurationField;

        private StrictDateType periodEndDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateType PeriodStartDate
        {
            get
            {
                return this.periodStartDateField;
            }
            set
            {
                this.periodStartDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration", Order = 1)]
        public string PeriodDuration
        {
            get
            {
                return this.periodDurationField;
            }
            set
            {
                this.periodDurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public StrictDateType PeriodEndDate
        {
            get
            {
                return this.periodEndDateField;
            }
            set
            {
                this.periodEndDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionInterruptionType : AbstractPeriodType
    {

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] sanctionInterruptionRemarksField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] SanctionInterruptionRemarks
        {
            get
            {
                return this.sanctionInterruptionRemarksField;
            }
            set
            {
                this.sanctionInterruptionRemarksField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionSuspensionType : AbstractPeriodType
    {

        private SanctionTypeOfSuspensionExternalReferenceType sanctionSuspensionTypeReferenceField;

        private string sanctionProbationDurationField;

        private string sanctionDurationSuspendedPartField;

        private PositiveDecimalType sanctionSuspensionPartiallySuspendedAmountField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] sanctionSuspensionRemarksField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public SanctionTypeOfSuspensionExternalReferenceType SanctionSuspensionTypeReference
        {
            get
            {
                return this.sanctionSuspensionTypeReferenceField;
            }
            set
            {
                this.sanctionSuspensionTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration", Order = 1)]
        public string SanctionProbationDuration
        {
            get
            {
                return this.sanctionProbationDurationField;
            }
            set
            {
                this.sanctionProbationDurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration", Order = 2)]
        public string SanctionDurationSuspendedPart
        {
            get
            {
                return this.sanctionDurationSuspendedPartField;
            }
            set
            {
                this.sanctionDurationSuspendedPartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public PositiveDecimalType SanctionSuspensionPartiallySuspendedAmount
        {
            get
            {
                return this.sanctionSuspensionPartiallySuspendedAmountField;
            }
            set
            {
                this.sanctionSuspensionPartiallySuspendedAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] SanctionSuspensionRemarks
        {
            get
            {
                return this.sanctionSuspensionRemarksField;
            }
            set
            {
                this.sanctionSuspensionRemarksField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class PositiveDecimalType
    {

        private string positiveDecimalUnitField;

        private string positiveDecimalFractionField;

        private string functionalErrorReferenceIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger", Order = 0)]
        public string PositiveDecimalUnit
        {
            get
            {
                return this.positiveDecimalUnitField;
            }
            set
            {
                this.positiveDecimalUnitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 1)]
        public string PositiveDecimalFraction
        {
            get
            {
                return this.positiveDecimalFractionField;
            }
            set
            {
                this.positiveDecimalFractionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionPeriodType : AbstractPeriodType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class RestrictedPositiveIntegerWithErrorsType
    {

        private string functionalErrorReferenceIdentifierField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, DataType = "ID")]
        public string functionalErrorReferenceIdentifier
        {
            get
            {
                return this.functionalErrorReferenceIdentifierField;
            }
            set
            {
                this.functionalErrorReferenceIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "positiveInteger")]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DecisionToSanctionsRelationshipType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DecisionToOffencesRelationshipType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionToSanctionsRelationshipType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SanctionToOffencesRelationshipType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConvictionToConvictionsRelationshipType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public abstract partial class AbstractRelationshipType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class DecisionToSanctionsRelationshipType : AbstractRelationshipType
    {

        private string decisionField;

        private string[] sanctionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 0)]
        public string Decision
        {
            get
            {
                return this.decisionField;
            }
            set
            {
                this.decisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Sanction", DataType = "IDREF", Order = 1)]
        public string[] Sanction
        {
            get
            {
                return this.sanctionField;
            }
            set
            {
                this.sanctionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class DecisionToOffencesRelationshipType : AbstractRelationshipType
    {

        private string decisionField;

        private string[] offenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 0)]
        public string Decision
        {
            get
            {
                return this.decisionField;
            }
            set
            {
                this.decisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Offence", DataType = "IDREF", Order = 1)]
        public string[] Offence
        {
            get
            {
                return this.offenceField;
            }
            set
            {
                this.offenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionToSanctionsRelationshipType : AbstractRelationshipType
    {

        private string sourceSanctionField;

        private string[] destinationSanctionField;

        private string relationshipTypeReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 0)]
        public string SourceSanction
        {
            get
            {
                return this.sourceSanctionField;
            }
            set
            {
                this.sourceSanctionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DestinationSanction", DataType = "IDREF", Order = 1)]
        public string[] DestinationSanction
        {
            get
            {
                return this.destinationSanctionField;
            }
            set
            {
                this.destinationSanctionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string relationshipTypeReference
        {
            get
            {
                return this.relationshipTypeReferenceField;
            }
            set
            {
                this.relationshipTypeReferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class SanctionToOffencesRelationshipType : AbstractRelationshipType
    {

        private string sanctionField;

        private string[] offenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 0)]
        public string Sanction
        {
            get
            {
                return this.sanctionField;
            }
            set
            {
                this.sanctionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Offence", DataType = "IDREF", Order = 1)]
        public string[] Offence
        {
            get
            {
                return this.offenceField;
            }
            set
            {
                this.offenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class ConvictionToConvictionsRelationshipType : AbstractRelationshipType
    {

        private string sourceConvictionField;

        private StructuredConvictionReferenceType[] destinationConvictionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "IDREF", Order = 0)]
        public string SourceConviction
        {
            get
            {
                return this.sourceConvictionField;
            }
            set
            {
                this.sourceConvictionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DestinationConviction", Order = 1)]
        public StructuredConvictionReferenceType[] DestinationConviction
        {
            get
            {
                return this.destinationConvictionField;
            }
            set
            {
                this.destinationConvictionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class StructuredConvictionReferenceType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ECRISConvictionReference", typeof(ConvictionExternalReferenceType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("ExternalConviction", typeof(StructuredConvictionReferenceTypeExternalConviction), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("NJRConvictionReference", typeof(NJRConvictionExternalReferenceType), Order = 0)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class StructuredConvictionReferenceTypeExternalConviction
    {

        private DecidingAuthorityType convictingAuthorityField;

        private string convictionFileNumberField;

        private StrictDateType convictionDecisionFinalDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public DecidingAuthorityType ConvictingAuthority
        {
            get
            {
                return this.convictingAuthorityField;
            }
            set
            {
                this.convictingAuthorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string ConvictionFileNumber
        {
            get
            {
                return this.convictionFileNumberField;
            }
            set
            {
                this.convictionFileNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public StrictDateType ConvictionDecisionFinalDate
        {
            get
            {
                return this.convictionDecisionFinalDateField;
            }
            set
            {
                this.convictionDecisionFinalDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class UpdateConvictionReferenceType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ECRISConvictionReference", typeof(string), DataType = "IDREF", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("ExternalConviction", typeof(StructuredConvictionReferenceTypeExternalConviction), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("NJRConvictionReference", typeof(NJRConvictionExternalReferenceType), Order = 0)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestingAuthorityType
    {

        private RequestingAuthorityTypeExternalReferenceType requestingAuthorityTypeReferenceField;

        private RestrictedStringType50Chars requestingAuthorityCodeField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] requestingAuthorityNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RequestingAuthorityTypeExternalReferenceType RequestingAuthorityTypeReference
        {
            get
            {
                return this.requestingAuthorityTypeReferenceField;
            }
            set
            {
                this.requestingAuthorityTypeReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public RestrictedStringType50Chars RequestingAuthorityCode
        {
            get
            {
                return this.requestingAuthorityCodeField;
            }
            set
            {
                this.requestingAuthorityCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] RequestingAuthorityName
        {
            get
            {
                return this.requestingAuthorityNameField;
            }
            set
            {
                this.requestingAuthorityNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public enum MemberStateCodeType
    {

        /// <remarks/>
        AT,

        /// <remarks/>
        BE,

        /// <remarks/>
        BG,

        /// <remarks/>
        CY,

        /// <remarks/>
        CZ,

        /// <remarks/>
        DE,

        /// <remarks/>
        DK,

        /// <remarks/>
        EE,

        /// <remarks/>
        ES,

        /// <remarks/>
        FI,

        /// <remarks/>
        FR,

        /// <remarks/>
        GB,

        /// <remarks/>
        GR,

        /// <remarks/>
        HR,

        /// <remarks/>
        HU,

        /// <remarks/>
        IE,

        /// <remarks/>
        IT,

        /// <remarks/>
        LT,

        /// <remarks/>
        LU,

        /// <remarks/>
        LV,

        /// <remarks/>
        MT,

        /// <remarks/>
        NL,

        /// <remarks/>
        PL,

        /// <remarks/>
        PT,

        /// <remarks/>
        RO,

        /// <remarks/>
        SE,

        /// <remarks/>
        SI,

        /// <remarks/>
        SK,

        /// <remarks/>
        ZZ,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class FunctionalErrorType
    {

        private string functionalErrorCodeField;

        private UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] functionalErrorAdditionalInformationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "ID", Order = 0)]
        public string FunctionalErrorCode
        {
            get
            {
                return this.functionalErrorCodeField;
            }
            set
            {
                this.functionalErrorCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[] FunctionalErrorAdditionalInformation
        {
            get
            {
                return this.functionalErrorAdditionalInformationField;
            }
            set
            {
                this.functionalErrorAdditionalInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public enum AdditionalInformationRequestedEnumeration
    {

        /// <remarks/>
        Forename,

        /// <remarks/>
        Surname,

        /// <remarks/>
        SecondSurname,

        /// <remarks/>
        Sex,

        /// <remarks/>
        BirthDate,

        /// <remarks/>
        BirthPlace,

        /// <remarks/>
        Nationality,

        /// <remarks/>
        FormerName,

        /// <remarks/>
        MotherName,

        /// <remarks/>
        FatherName,

        /// <remarks/>
        IdentityNumber,

        /// <remarks/>
        IdentificationDocument,

        /// <remarks/>
        Address,

        /// <remarks/>
        Alias,

        /// <remarks/>
        Fingerprints,

        /// <remarks/>
        MoreInformationOnRequestPurpose,
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RestrictedIdentifiableMessageType : IdentifiableMessageType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AbstractMessageType))]
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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RestrictedIdentifiableMessageType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class IdentifiableMessageType
    {

        private string messageIdentifierField;

        private string messageEcrisIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MessageIdentifier
        {
            get
            {
                return this.messageIdentifierField;
            }
            set
            {
                this.messageIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string MessageEcrisIdentifier
        {
            get
            {
                return this.messageEcrisIdentifierField;
            }
            set
            {
                this.messageEcrisIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EcrisMessageType
    {

        /// <remarks/>
        REQ,

        /// <remarks/>
        RDL,

        /// <remarks/>
        RRS,

        /// <remarks/>
        NOT,

        /// <remarks/>
        NRS,

        /// <remarks/>
        RAI,

        /// <remarks/>
        RAR,

        /// <remarks/>
        FEM,

        /// <remarks/>
        CAN,
    }


    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FolderType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class IdentifiableFolderType
    {

        private string folderIdentifierField;

        private string folderNameField;

        private bool folderPredefinedField;

        private bool folderPredefinedFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FolderIdentifier
        {
            get
            {
                return this.folderIdentifierField;
            }
            set
            {
                this.folderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string FolderName
        {
            get
            {
                return this.folderNameField;
            }
            set
            {
                this.folderNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public bool FolderPredefined
        {
            get
            {
                return this.folderPredefinedField;
            }
            set
            {
                this.folderPredefinedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FolderPredefinedSpecified
        {
            get
            {
                return this.folderPredefinedFieldSpecified;
            }
            set
            {
                this.folderPredefinedFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class FolderType : IdentifiableFolderType
    {

        private string parentFolderIdentifierField;

        private string precedingFolderIdentifierField;

        private string followingFolderIdentifierField;

        private FolderType[] folderContainedFoldersField;

        private int folderNumberOfMessagesContainedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ParentFolderIdentifier
        {
            get
            {
                return this.parentFolderIdentifierField;
            }
            set
            {
                this.parentFolderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string PrecedingFolderIdentifier
        {
            get
            {
                return this.precedingFolderIdentifierField;
            }
            set
            {
                this.precedingFolderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string FollowingFolderIdentifier
        {
            get
            {
                return this.followingFolderIdentifierField;
            }
            set
            {
                this.followingFolderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FolderContainedFolders", Order = 3)]
        public FolderType[] FolderContainedFolders
        {
            get
            {
                return this.folderContainedFoldersField;
            }
            set
            {
                this.folderContainedFoldersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int FolderNumberOfMessagesContained
        {
            get
            {
                return this.folderNumberOfMessagesContainedField;
            }
            set
            {
                this.folderNumberOfMessagesContainedField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestAdditionalInfoMessageShortViewType : MessageShortViewType
    {

        private AdditionalInformationRequestedEnumeration[] requestAdditionalInfoShortViewInformationRequestedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RequestAdditionalInfoShortViewInformationRequested", Order = 0)]
        public AdditionalInformationRequestedEnumeration[] RequestAdditionalInfoShortViewInformationRequested
        {
            get
            {
                return this.requestAdditionalInfoShortViewInformationRequestedField;
            }
            set
            {
                this.requestAdditionalInfoShortViewInformationRequestedField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MessageRelatedToRequestShortViewType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class ResponseMessageShortViewType : MessageShortViewType
    {

        private string responseMessageShortViewResponseCodeField;

        private string originalMessageEcrisIdentifierField;

        private string originalMessageIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ResponseMessageShortViewResponseCode
        {
            get
            {
                return this.responseMessageShortViewResponseCodeField;
            }
            set
            {
                this.responseMessageShortViewResponseCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string OriginalMessageEcrisIdentifier
        {
            get
            {
                return this.originalMessageEcrisIdentifierField;
            }
            set
            {
                this.originalMessageEcrisIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string OriginalMessageIdentifier
        {
            get
            {
                return this.originalMessageIdentifierField;
            }
            set
            {
                this.originalMessageIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class MessageRelatedToRequestShortViewType : ResponseMessageShortViewType
    {

        private YesNoUnknownStringEnumerationType requestMessageShortViewUrgencyField;

        private RequestPurposeExternalReferenceType requestMessageShortViewPurposeCategoryReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public YesNoUnknownStringEnumerationType RequestMessageShortViewUrgency
        {
            get
            {
                return this.requestMessageShortViewUrgencyField;
            }
            set
            {
                this.requestMessageShortViewUrgencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public RequestPurposeExternalReferenceType RequestMessageShortViewPurposeCategoryReference
        {
            get
            {
                return this.requestMessageShortViewPurposeCategoryReferenceField;
            }
            set
            {
                this.requestMessageShortViewPurposeCategoryReferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestMessageShortViewType : MessageShortViewType
    {

        private YesNoUnknownStringEnumerationType requestMessageShortViewUrgencyField;

        private RequestPurposeExternalReferenceType requestMessageShortViewPurposeCategoryReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public YesNoUnknownStringEnumerationType RequestMessageShortViewUrgency
        {
            get
            {
                return this.requestMessageShortViewUrgencyField;
            }
            set
            {
                this.requestMessageShortViewUrgencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public RequestPurposeExternalReferenceType RequestMessageShortViewPurposeCategoryReference
        {
            get
            {
                return this.requestMessageShortViewPurposeCategoryReferenceField;
            }
            set
            {
                this.requestMessageShortViewPurposeCategoryReferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class CancellationMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class FunctionalErrorMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestAdditionalInfoResponseMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestAdditionalInfoMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class NotificationResponseMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class NotificationMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestResponseMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestDeadlineMessageType : AbstractBusinessMessageType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RequestMessageType : AbstractBusinessMessageType
    {
        [XmlIgnoreAttribute]
        public string EcrisMsgId { get; set; }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class PerformFunctionalValidationWSInputDataType
    {

        private AbstractBusinessMessageType ecrisRiMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractBusinessMessageType EcrisRiMessage
        {
            get
            {
                return this.ecrisRiMessageField;
            }
            set
            {
                this.ecrisRiMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SetSystemAvailabilityWSInputDataType
    {

        private bool isSystemAvailableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool IsSystemAvailable
        {
            get
            {
                return this.isSystemAvailableField;
            }
            set
            {
                this.isSystemAvailableField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListBackendLogsWSOutputDataType
    {

        private BackendLogWSOutputDataType[] logsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Logs", Order = 0)]
        public BackendLogWSOutputDataType[] Logs
        {
            get
            {
                return this.logsField;
            }
            set
            {
                this.logsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class BackendLogWSOutputDataType
    {

        private string logField;

        private System.DateTime logsTimestampField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Log
        {
            get
            {
                return this.logField;
            }
            set
            {
                this.logField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public System.DateTime LogsTimestamp
        {
            get
            {
                return this.logsTimestampField;
            }
            set
            {
                this.logsTimestampField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListBackendLogsWSInputDataType
    {

        private BackendLogsTypeEnumeration logTypeField;

        private MemberStateCodeType memberStateEndpointMemberStateCodeField;

        private bool memberStateEndpointMemberStateCodeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public BackendLogsTypeEnumeration LogType
        {
            get
            {
                return this.logTypeField;
            }
            set
            {
                this.logTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MemberStateCodeType MemberStateEndpointMemberStateCode
        {
            get
            {
                return this.memberStateEndpointMemberStateCodeField;
            }
            set
            {
                this.memberStateEndpointMemberStateCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MemberStateEndpointMemberStateCodeSpecified
        {
            get
            {
                return this.memberStateEndpointMemberStateCodeFieldSpecified;
            }
            set
            {
                this.memberStateEndpointMemberStateCodeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum BackendLogsTypeEnumeration
    {

        /// <remarks/>
        ECRIS,

        /// <remarks/>
        COMMUNICATION,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SystemAvailabilityWSOutputDataType
    {

        private bool isSystemAvailableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool IsSystemAvailable
        {
            get
            {
                return this.isSystemAvailableField;
            }
            set
            {
                this.isSystemAvailableField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MoveDownRuleWSInputDataType
    {

        private string ruleIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string RuleIdentifier
        {
            get
            {
                return this.ruleIdentifierField;
            }
            set
            {
                this.ruleIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MoveUpRuleWSInputDataType
    {

        private string ruleIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string RuleIdentifier
        {
            get
            {
                return this.ruleIdentifierField;
            }
            set
            {
                this.ruleIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteRuleWSInputDataType
    {

        private string ruleIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string RuleIdentifier
        {
            get
            {
                return this.ruleIdentifierField;
            }
            set
            {
                this.ruleIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreRuleWSInputDataType
    {

        private RuleType ruleField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RuleType Rule
        {
            get
            {
                return this.ruleField;
            }
            set
            {
                this.ruleField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RuleType
    {

        private string ruleIdentifierField;

        private string ruleNameField;

        private int orderIdField;

        private bool orderIdFieldSpecified;

        private EcrisMessageTypeOrAlias[] messageTypesField;

        private MemberStateCodeType[] memberStatesField;

        private string surnameInitialsFromField;

        private string surnameInitialsToField;

        private FolderType targetFolderField;

        private int personGenderField;

        private bool personGenderFieldSpecified;

        private RuleTransactionType transactionTypeField;

        private bool transactionTypeFieldSpecified;

        private MessageDirection triggerOnField;

        private RuleMoveType moveTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string RuleIdentifier
        {
            get
            {
                return this.ruleIdentifierField;
            }
            set
            {
                this.ruleIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string RuleName
        {
            get
            {
                return this.ruleNameField;
            }
            set
            {
                this.ruleNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int OrderId
        {
            get
            {
                return this.orderIdField;
            }
            set
            {
                this.orderIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrderIdSpecified
        {
            get
            {
                return this.orderIdFieldSpecified;
            }
            set
            {
                this.orderIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MessageTypes", Order = 3)]
        public EcrisMessageTypeOrAlias[] MessageTypes
        {
            get
            {
                return this.messageTypesField;
            }
            set
            {
                this.messageTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MemberStates", Order = 4)]
        public MemberStateCodeType[] MemberStates
        {
            get
            {
                return this.memberStatesField;
            }
            set
            {
                this.memberStatesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string SurnameInitialsFrom
        {
            get
            {
                return this.surnameInitialsFromField;
            }
            set
            {
                this.surnameInitialsFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string SurnameInitialsTo
        {
            get
            {
                return this.surnameInitialsToField;
            }
            set
            {
                this.surnameInitialsToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public FolderType TargetFolder
        {
            get
            {
                return this.targetFolderField;
            }
            set
            {
                this.targetFolderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public int PersonGender
        {
            get
            {
                return this.personGenderField;
            }
            set
            {
                this.personGenderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PersonGenderSpecified
        {
            get
            {
                return this.personGenderFieldSpecified;
            }
            set
            {
                this.personGenderFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public RuleTransactionType TransactionType
        {
            get
            {
                return this.transactionTypeField;
            }
            set
            {
                this.transactionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TransactionTypeSpecified
        {
            get
            {
                return this.transactionTypeFieldSpecified;
            }
            set
            {
                this.transactionTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public MessageDirection TriggerOn
        {
            get
            {
                return this.triggerOnField;
            }
            set
            {
                this.triggerOnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public RuleMoveType MoveType
        {
            get
            {
                return this.moveTypeField;
            }
            set
            {
                this.moveTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class EcrisMessageTypeOrAlias
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MessageType", typeof(EcrisMessageTypeOrAliasMessageType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("NotificationResponseMessageType", typeof(EcrisMessageTypeOrAliasNotificationResponseMessageType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("RequestAdditionalInfoResponseMessageType", typeof(EcrisMessageTypeOrAliasRequestAdditionalInfoResponseMessageType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("RequestResponseMessageType", typeof(EcrisMessageTypeOrAliasRequestResponseMessageType), Order = 0)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EcrisMessageTypeOrAliasMessageType
    {

        /// <remarks/>
        REQ,

        /// <remarks/>
        RDL,

        /// <remarks/>
        NOT,

        /// <remarks/>
        RAI,

        /// <remarks/>
        FEM,

        /// <remarks/>
        CAN,

        /// <remarks/>
        NRS,

        /// <remarks/>
        RRS,

        /// <remarks/>
        RAR,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class EcrisMessageTypeOrAliasNotificationResponseMessageType
    {

        private EcrisMessageTypeOrAliasNotificationResponseMessageTypeMessageType messageTypeField;

        private NotificationResponseTypeExternalReferenceType[] messageResponseCodesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public EcrisMessageTypeOrAliasNotificationResponseMessageTypeMessageType MessageType
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
        [System.Xml.Serialization.XmlElementAttribute("MessageResponseCodes", Order = 1)]
        public NotificationResponseTypeExternalReferenceType[] MessageResponseCodes
        {
            get
            {
                return this.messageResponseCodesField;
            }
            set
            {
                this.messageResponseCodesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EcrisMessageTypeOrAliasNotificationResponseMessageTypeMessageType
    {

        /// <remarks/>
        NRS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class EcrisMessageTypeOrAliasRequestAdditionalInfoResponseMessageType
    {

        private EcrisMessageTypeOrAliasRequestAdditionalInfoResponseMessageTypeMessageType messageTypeField;

        private RequestAdditionalInfoTypeExternalReferenceType[] messageResponseCodesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public EcrisMessageTypeOrAliasRequestAdditionalInfoResponseMessageTypeMessageType MessageType
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
        [System.Xml.Serialization.XmlElementAttribute("MessageResponseCodes", Order = 1)]
        public RequestAdditionalInfoTypeExternalReferenceType[] MessageResponseCodes
        {
            get
            {
                return this.messageResponseCodesField;
            }
            set
            {
                this.messageResponseCodesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EcrisMessageTypeOrAliasRequestAdditionalInfoResponseMessageTypeMessageType
    {

        /// <remarks/>
        RAR,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class EcrisMessageTypeOrAliasRequestResponseMessageType
    {

        private EcrisMessageTypeOrAliasRequestResponseMessageTypeMessageType messageTypeField;

        private RequestResponseTypeExternalReferenceType[] messageResponseCodesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public EcrisMessageTypeOrAliasRequestResponseMessageTypeMessageType MessageType
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
        [System.Xml.Serialization.XmlElementAttribute("MessageResponseCodes", Order = 1)]
        public RequestResponseTypeExternalReferenceType[] MessageResponseCodes
        {
            get
            {
                return this.messageResponseCodesField;
            }
            set
            {
                this.messageResponseCodesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum EcrisMessageTypeOrAliasRequestResponseMessageTypeMessageType
    {

        /// <remarks/>
        RRS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public enum RuleTransactionType
    {

        /// <remarks/>
        NOT,

        /// <remarks/>
        REQ,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public enum MessageDirection
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public enum RuleMoveType
    {

        /// <remarks/>
        MESSAGE,

        /// <remarks/>
        TRANSACTION,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListAssignedFolderRolesWSOutputDataType
    {

        private RoleType[] roleDataTypeField;

        private int roleCountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleDataType", Order = 0)]
        public RoleType[] RoleDataType
        {
            get
            {
                return this.roleDataTypeField;
            }
            set
            {
                this.roleDataTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int RoleCount
        {
            get
            {
                return this.roleCountField;
            }
            set
            {
                this.roleCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RoleType
    {

        private string roleIdentifierField;

        private string roleNameField;

        private AbstractAccessRightType[] accessRightsField;

        private string[] eventCodesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string RoleIdentifier
        {
            get
            {
                return this.roleIdentifierField;
            }
            set
            {
                this.roleIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string RoleName
        {
            get
            {
                return this.roleNameField;
            }
            set
            {
                this.roleNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AccessRights", Order = 2)]
        public AbstractAccessRightType[] AccessRights
        {
            get
            {
                return this.accessRightsField;
            }
            set
            {
                this.accessRightsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EventCodes", Order = 3)]
        public string[] EventCodes
        {
            get
            {
                return this.eventCodesField;
            }
            set
            {
                this.eventCodesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ViewAccessRightsType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SendAccessRightsType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EditAccessRightsType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BasicAccessRightsType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public abstract partial class AbstractAccessRightType
    {

        private string accessRightTypeField;

        private string[] messageTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string AccessRightType
        {
            get
            {
                return this.accessRightTypeField;
            }
            set
            {
                this.accessRightTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MessageType", Order = 1)]
        public string[] MessageType
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class ViewAccessRightsType : AbstractAccessRightType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class SendAccessRightsType : AbstractAccessRightType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class EditAccessRightsType : AbstractAccessRightType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class BasicAccessRightsType : AbstractAccessRightType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListAssignedFolderRolesWSInputDataType
    {

        private string folderIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FolderIdentifier
        {
            get
            {
                return this.folderIdentifierField;
            }
            set
            {
                this.folderIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class AssignFolderRolesWSInputDataType
    {

        private string folderIdentifierField;

        private string[] rolesIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FolderIdentifier
        {
            get
            {
                return this.folderIdentifierField;
            }
            set
            {
                this.folderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RolesIdentifier", DataType = "positiveInteger", Order = 1)]
        public string[] RolesIdentifier
        {
            get
            {
                return this.rolesIdentifierField;
            }
            set
            {
                this.rolesIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RuleViewListType
    {

        private int totalCountField;

        private RuleType[] ruleField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int TotalCount
        {
            get
            {
                return this.totalCountField;
            }
            set
            {
                this.totalCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Rule", Order = 1)]
        public RuleType[] Rule
        {
            get
            {
                return this.ruleField;
            }
            set
            {
                this.ruleField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RuleListWSOutputDataType
    {

        private RuleViewListType ruleViewListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RuleViewListType RuleViewList
        {
            get
            {
                return this.ruleViewListField;
            }
            set
            {
                this.ruleViewListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class UserViewListType
    {

        private int totalCountField;

        private UserType[] userField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int TotalCount
        {
            get
            {
                return this.totalCountField;
            }
            set
            {
                this.totalCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("User", Order = 1)]
        public UserType[] User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class UserType
    {

        private string userIdentifierField;

        private string userNameField;

        private string userPasswordField;

        private string userLanguageField;

        private bool userLoggedInField;

        private bool userLoggedInFieldSpecified;

        private System.DateTime userLoginDateField;

        private bool userLoginDateFieldSpecified;

        private System.DateTime userPasswordDateField;

        private bool userPasswordDateFieldSpecified;

        private NameTextType forenameField;

        private NameTextType surnameField;

        private NameTextType secondSurnameField;

        private RoleType[] userRolesField;

        private bool userEnabledField;

        private bool userEnabledFieldSpecified;

        private bool userPasswordNeverExpiresField;

        private bool userPasswordNeverExpiresFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string UserIdentifier
        {
            get
            {
                return this.userIdentifierField;
            }
            set
            {
                this.userIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string UserPassword
        {
            get
            {
                return this.userPasswordField;
            }
            set
            {
                this.userPasswordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string UserLanguage
        {
            get
            {
                return this.userLanguageField;
            }
            set
            {
                this.userLanguageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public bool UserLoggedIn
        {
            get
            {
                return this.userLoggedInField;
            }
            set
            {
                this.userLoggedInField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserLoggedInSpecified
        {
            get
            {
                return this.userLoggedInFieldSpecified;
            }
            set
            {
                this.userLoggedInFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public System.DateTime UserLoginDate
        {
            get
            {
                return this.userLoginDateField;
            }
            set
            {
                this.userLoginDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserLoginDateSpecified
        {
            get
            {
                return this.userLoginDateFieldSpecified;
            }
            set
            {
                this.userLoginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public System.DateTime UserPasswordDate
        {
            get
            {
                return this.userPasswordDateField;
            }
            set
            {
                this.userPasswordDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserPasswordDateSpecified
        {
            get
            {
                return this.userPasswordDateFieldSpecified;
            }
            set
            {
                this.userPasswordDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public NameTextType Forename
        {
            get
            {
                return this.forenameField;
            }
            set
            {
                this.forenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public NameTextType Surname
        {
            get
            {
                return this.surnameField;
            }
            set
            {
                this.surnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public NameTextType SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UserRoles", Order = 10)]
        public RoleType[] UserRoles
        {
            get
            {
                return this.userRolesField;
            }
            set
            {
                this.userRolesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public bool UserEnabled
        {
            get
            {
                return this.userEnabledField;
            }
            set
            {
                this.userEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserEnabledSpecified
        {
            get
            {
                return this.userEnabledFieldSpecified;
            }
            set
            {
                this.userEnabledFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public bool UserPasswordNeverExpires
        {
            get
            {
                return this.userPasswordNeverExpiresField;
            }
            set
            {
                this.userPasswordNeverExpiresField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserPasswordNeverExpiresSpecified
        {
            get
            {
                return this.userPasswordNeverExpiresFieldSpecified;
            }
            set
            {
                this.userPasswordNeverExpiresFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserListWSOutputDataType
    {

        private UserViewListType userViewListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public UserViewListType UserViewList
        {
            get
            {
                return this.userViewListField;
            }
            set
            {
                this.userViewListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RuleListWSInputDataType
    {

        private RuleListWSInputDataTypeRulesSortedBy rulesSortedByField;

        private bool rulesSortedByFieldSpecified;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RuleListWSInputDataTypeRulesSortedBy RulesSortedBy
        {
            get
            {
                return this.rulesSortedByField;
            }
            set
            {
                this.rulesSortedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RulesSortedBySpecified
        {
            get
            {
                return this.rulesSortedByFieldSpecified;
            }
            set
            {
                this.rulesSortedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 2)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum RuleListWSInputDataTypeRulesSortedBy
    {

        /// <remarks/>
        OrderAsc,

        /// <remarks/>
        OrderDesc,

        /// <remarks/>
        RuleNameAsc,

        /// <remarks/>
        RuleNameDesc,

        /// <remarks/>
        MessageTypeAsc,

        /// <remarks/>
        MessageTypeDesc,

        /// <remarks/>
        MemberStateAsc,

        /// <remarks/>
        MemberStateDesc,

        /// <remarks/>
        SurnameInitialsAsc,

        /// <remarks/>
        SurnameInitialsDesc,

        /// <remarks/>
        TargetFolderAsc,

        /// <remarks/>
        TargetFolderDesc,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserListWSInputDataType
    {

        private bool getAdministratorField;

        private UserListWSInputDataTypeUsersSortedBy usersSortedByField;

        private bool usersSortedByFieldSpecified;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool getAdministrator
        {
            get
            {
                return this.getAdministratorField;
            }
            set
            {
                this.getAdministratorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public UserListWSInputDataTypeUsersSortedBy UsersSortedBy
        {
            get
            {
                return this.usersSortedByField;
            }
            set
            {
                this.usersSortedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UsersSortedBySpecified
        {
            get
            {
                return this.usersSortedByFieldSpecified;
            }
            set
            {
                this.usersSortedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 3)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum UserListWSInputDataTypeUsersSortedBy
    {

        /// <remarks/>
        UserNameAsc,

        /// <remarks/>
        UserNameDesc,

        /// <remarks/>
        FullNameAsc,

        /// <remarks/>
        FullNameDesc,

        /// <remarks/>
        RolesAsc,

        /// <remarks/>
        RolesDesc,

        /// <remarks/>
        StatusAsc,

        /// <remarks/>
        StatusDesc,

        /// <remarks/>
        LoginDateAsc,

        /// <remarks/>
        LoginDateDesc,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DisableUserWSInputDataType
    {

        private string userIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string UserIdentifier
        {
            get
            {
                return this.userIdentifierField;
            }
            set
            {
                this.userIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class EnableUserWSInputDataType
    {

        private string userIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string UserIdentifier
        {
            get
            {
                return this.userIdentifierField;
            }
            set
            {
                this.userIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveUserWSInputDataType
    {

        private string userIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 0)]
        public string UserIdentifier
        {
            get
            {
                return this.userIdentifierField;
            }
            set
            {
                this.userIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserWSOutputDataType
    {

        private UserType userField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public UserType User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserWSInputDataType
    {

        private UserType userField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public UserType User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveUserRoleWSInputDataType
    {

        private RoleType roleDataTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RoleType RoleDataType
        {
            get
            {
                return this.roleDataTypeField;
            }
            set
            {
                this.roleDataTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreRoleWSInputDataType
    {

        private RoleType roleDataTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RoleType RoleDataType
        {
            get
            {
                return this.roleDataTypeField;
            }
            set
            {
                this.roleDataTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAvailableRolesWSOutputDataType
    {

        private RoleType[] roleDataTypeField;

        private int roleCountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleDataType", Order = 0)]
        public RoleType[] RoleDataType
        {
            get
            {
                return this.roleDataTypeField;
            }
            set
            {
                this.roleDataTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int RoleCount
        {
            get
            {
                return this.roleCountField;
            }
            set
            {
                this.roleCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAvailableRolesWSInputDataType
    {

        private RetrieveAvailableRolesWSInputDataTypeRolesSortedBy rolesSortedByField;

        private bool rolesSortedByFieldSpecified;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RetrieveAvailableRolesWSInputDataTypeRolesSortedBy RolesSortedBy
        {
            get
            {
                return this.rolesSortedByField;
            }
            set
            {
                this.rolesSortedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RolesSortedBySpecified
        {
            get
            {
                return this.rolesSortedByFieldSpecified;
            }
            set
            {
                this.rolesSortedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 2)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum RetrieveAvailableRolesWSInputDataTypeRolesSortedBy
    {

        /// <remarks/>
        RoleNameAsc,

        /// <remarks/>
        RoleNameDesc,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MemberStateCodeContainingWSInputDataType
    {

        private MemberStateCodeType memberStateCodeField;

        private string ecrisVersionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MemberStateCodeType MemberStateCode
        {
            get
            {
                return this.memberStateCodeField;
            }
            set
            {
                this.memberStateCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string EcrisVersion
        {
            get
            {
                return this.ecrisVersionField;
            }
            set
            {
                this.ecrisVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MemberStateEndpointType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class MemberStateEndpointStatusType
    {

        private MemberStateCodeType memberStateEndpointMemberStateCodeField;

        private bool memberStateEndpointCommunicationStatusField;

        private bool memberStateEndpointIsAliveStatusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MemberStateCodeType MemberStateEndpointMemberStateCode
        {
            get
            {
                return this.memberStateEndpointMemberStateCodeField;
            }
            set
            {
                this.memberStateEndpointMemberStateCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public bool MemberStateEndpointCommunicationStatus
        {
            get
            {
                return this.memberStateEndpointCommunicationStatusField;
            }
            set
            {
                this.memberStateEndpointCommunicationStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public bool MemberStateEndpointIsAliveStatus
        {
            get
            {
                return this.memberStateEndpointIsAliveStatusField;
            }
            set
            {
                this.memberStateEndpointIsAliveStatusField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class MemberStateEndpointType : MemberStateEndpointStatusType
    {

        private string memberStateEndpointUrlField;

        private MemberStateEndpointTypeMemberStateEndpointEcrisVersion memberStateEndpointEcrisVersionField;

        private string memberStateEndpointLagField;

        private System.DateTime memberStateEndpointLastExchangeTimestampField;

        private bool memberStateEndpointLastExchangeTimestampFieldSpecified;

        private BinaryAttachmentSupportEnum binaryAttachmentSupportField;

        private bool binaryAttachmentSupportFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MemberStateEndpointUrl
        {
            get
            {
                return this.memberStateEndpointUrlField;
            }
            set
            {
                this.memberStateEndpointUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MemberStateEndpointTypeMemberStateEndpointEcrisVersion MemberStateEndpointEcrisVersion
        {
            get
            {
                return this.memberStateEndpointEcrisVersionField;
            }
            set
            {
                this.memberStateEndpointEcrisVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 2)]
        public string MemberStateEndpointLag
        {
            get
            {
                return this.memberStateEndpointLagField;
            }
            set
            {
                this.memberStateEndpointLagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public System.DateTime MemberStateEndpointLastExchangeTimestamp
        {
            get
            {
                return this.memberStateEndpointLastExchangeTimestampField;
            }
            set
            {
                this.memberStateEndpointLastExchangeTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MemberStateEndpointLastExchangeTimestampSpecified
        {
            get
            {
                return this.memberStateEndpointLastExchangeTimestampFieldSpecified;
            }
            set
            {
                this.memberStateEndpointLastExchangeTimestampFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public BinaryAttachmentSupportEnum BinaryAttachmentSupport
        {
            get
            {
                return this.binaryAttachmentSupportField;
            }
            set
            {
                this.binaryAttachmentSupportField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BinaryAttachmentSupportSpecified
        {
            get
            {
                return this.binaryAttachmentSupportFieldSpecified;
            }
            set
            {
                this.binaryAttachmentSupportFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public enum MemberStateEndpointTypeMemberStateEndpointEcrisVersion
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("v1.0")]
        v10,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public enum BinaryAttachmentSupportEnum
    {

        /// <remarks/>
        NOFP,

        /// <remarks/>
        PUSH,

        /// <remarks/>
        PULL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveMemberStateEndpointListWSOutputDataType
    {

        private MemberStateEndpointType[] memberStateEndpointListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MemberStateEndpoint", IsNullable = false)]
        public MemberStateEndpointType[] MemberStateEndpointList
        {
            get
            {
                return this.memberStateEndpointListField;
            }
            set
            {
                this.memberStateEndpointListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class MessageTransactionType
    {

        private System.DateTime messageTransactionDeadlineField;

        private bool messageTransactionDeadlineFieldSpecified;

        private MessageShortViewType[] messageTransactionMessageListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public System.DateTime MessageTransactionDeadline
        {
            get
            {
                return this.messageTransactionDeadlineField;
            }
            set
            {
                this.messageTransactionDeadlineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageTransactionDeadlineSpecified
        {
            get
            {
                return this.messageTransactionDeadlineFieldSpecified;
            }
            set
            {
                this.messageTransactionDeadlineFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Message", IsNullable = false)]
        public MessageShortViewType[] MessageTransactionMessageList
        {
            get
            {
                return this.messageTransactionMessageListField;
            }
            set
            {
                this.messageTransactionMessageListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetTransactionForMessageWSOutputDataType
    {

        private MessageTransactionType messageTransactionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MessageTransactionType MessageTransaction
        {
            get
            {
                return this.messageTransactionField;
            }
            set
            {
                this.messageTransactionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetTransactionForMessageWSInputDataType
    {

        private string messageIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MessageIdentifier
        {
            get
            {
                return this.messageIdentifierField;
            }
            set
            {
                this.messageIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class StatisticYearType
    {

        private string yearField;

        private string[] monthsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear", Order = 0)]
        public string Year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Months", DataType = "gMonth", Order = 1)]
        public string[] Months
        {
            get
            {
                return this.monthsField;
            }
            set
            {
                this.monthsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class CalculateNextExportDeadlineWSOutputDataType
    {

        private StrictDateType exportDeadlineField;

        private StatisticalIndicatorTypeEnumerationType exportIndicatorTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateType ExportDeadline
        {
            get
            {
                return this.exportDeadlineField;
            }
            set
            {
                this.exportDeadlineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StatisticalIndicatorTypeEnumerationType ExportIndicatorType
        {
            get
            {
                return this.exportIndicatorTypeField;
            }
            set
            {
                this.exportIndicatorTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum StatisticalIndicatorTypeEnumerationType
    {

        /// <remarks/>
        Yearly,

        /// <remarks/>
        Monthly,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/statistics-v1.0")]
    public partial class StatisticRecordType
    {

        private string itemField;

        private ItemChoiceType1 itemElementNameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReceivedFrom", typeof(string), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("SentTo", typeof(string), Order = 0)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger", Order = 2)]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/statistics-v1.0", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        ReceivedFrom,

        /// <remarks/>
        SentTo,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS/statistics-v1.0")]
    public partial class StatisticType
    {

        private string statisticIDField;

        private StatisticRecordType[] statisticRecordField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string StatisticID
        {
            get
            {
                return this.statisticIDField;
            }
            set
            {
                this.statisticIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StatisticRecord", Order = 1)]
        public StatisticRecordType[] StatisticRecord
        {
            get
            {
                return this.statisticRecordField;
            }
            set
            {
                this.statisticRecordField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DownloadStatisticsWSOutputDataType
    {

        private Statistics statisticsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://ec.europa.eu/ECRIS/statistics-v1.0", Order = 0)]
        public Statistics Statistics
        {
            get
            {
                return this.statisticsField;
            }
            set
            {
                this.statisticsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS/statistics-v1.0")]
    public partial class Statistics
    {

        private System.DateTime statisticsCreatedOnDateField;

        private System.DateTime statisticsLoggedFromTimestampField;

        private System.DateTime statisticsLoggedToTimestampField;

        private string statisticsProducedByMemberStateField;

        private StatisticType[] statisticField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public System.DateTime StatisticsCreatedOnDate
        {
            get
            {
                return this.statisticsCreatedOnDateField;
            }
            set
            {
                this.statisticsCreatedOnDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public System.DateTime StatisticsLoggedFromTimestamp
        {
            get
            {
                return this.statisticsLoggedFromTimestampField;
            }
            set
            {
                this.statisticsLoggedFromTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public System.DateTime StatisticsLoggedToTimestamp
        {
            get
            {
                return this.statisticsLoggedToTimestampField;
            }
            set
            {
                this.statisticsLoggedToTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string StatisticsProducedByMemberState
        {
            get
            {
                return this.statisticsProducedByMemberStateField;
            }
            set
            {
                this.statisticsProducedByMemberStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Statistic", Order = 4)]
        public StatisticType[] Statistic
        {
            get
            {
                return this.statisticField;
            }
            set
            {
                this.statisticField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DownloadStatisticsWSInputDataType
    {

        private AbstractStatisticsPeriodType statisticsPeriodField;

        private StatisticalIndicatorTypeEnumerationType[] statisticalIndicatorTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractStatisticsPeriodType StatisticsPeriod
        {
            get
            {
                return this.statisticsPeriodField;
            }
            set
            {
                this.statisticsPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StatisticalIndicatorType", Order = 1)]
        public StatisticalIndicatorTypeEnumerationType[] StatisticalIndicatorType
        {
            get
            {
                return this.statisticalIndicatorTypeField;
            }
            set
            {
                this.statisticalIndicatorTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class AdministrativeMessageWSInputDataType
    {

        private MemberStateCodeType memberStateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MemberStateCodeType MemberState
        {
            get
            {
                return this.memberStateField;
            }
            set
            {
                this.memberStateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveStoredQueriesWSOutputDataType
    {

        private QueryType[] queryListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Query", IsNullable = false)]
        public QueryType[] QueryList
        {
            get
            {
                return this.queryListField;
            }
            set
            {
                this.queryListField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DateRange
    {

        private DateType fromDateField;

        private DateType toDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public DateType FromDate
        {
            get
            {
                return this.fromDateField;
            }
            set
            {
                this.fromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public DateType ToDate
        {
            get
            {
                return this.toDateField;
            }
            set
            {
                this.toDateField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StrictDateRange
    {

        private StrictDateType fromDateField;

        private StrictDateType toDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateType FromDate
        {
            get
            {
                return this.fromDateField;
            }
            set
            {
                this.fromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StrictDateType ToDate
        {
            get
            {
                return this.toDateField;
            }
            set
            {
                this.toDateField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum MessageDateTypeEnumeration
    {

        /// <remarks/>
        LastUpdated,

        /// <remarks/>
        SentReceived,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveStoredQueryWSInputDataType
    {

        private string queryNameField;

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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreOrSendMessageWithCommentWSInputDataType
    {

        private object itemField;

        private string backendSessionIdField;

        private string commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SendMessageWSInputData", typeof(SendMessageWSInputDataType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("SendMessageZipWSInputData", typeof(SendMessageZipWSInputDataType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("StoreMessageWSInputData", typeof(StoreMessageWSInputDataType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("StoreMessageZipWSInputData", typeof(StoreMessageZipWSInputDataType), Order = 0)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string BackendSessionId
        {
            get
            {
                return this.backendSessionIdField;
            }
            set
            {
                this.backendSessionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageWSInputDataType
    {

        private AbstractMessageType ecrisMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageZipWSInputDataType
    {

        private byte[] ecrisRiZipField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 0)]
        public byte[] EcrisRiZip
        {
            get
            {
                return this.ecrisRiZipField;
            }
            set
            {
                this.ecrisRiZipField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageWSInputDataType
    {

        private AbstractMessageType ecrisRiMessageField;

        private string targetFolderIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractMessageType EcrisRiMessage
        {
            get
            {
                return this.ecrisRiMessageField;
            }
            set
            {
                this.ecrisRiMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string TargetFolderIdentifier
        {
            get
            {
                return this.targetFolderIdentifierField;
            }
            set
            {
                this.targetFolderIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageZipWSInputDataType
    {

        private byte[] ecrisRiZipField;

        private string targetFolderIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 0)]
        public byte[] EcrisRiZip
        {
            get
            {
                return this.ecrisRiZipField;
            }
            set
            {
                this.ecrisRiZipField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string TargetFolderIdentifier
        {
            get
            {
                return this.targetFolderIdentifierField;
            }
            set
            {
                this.targetFolderIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageZipWithCommentWSInputDataType
    {

        private SendMessageZipWSInputDataType sendMessageZipWSInputDataField;

        private string backendSessionIdField;

        private string commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public SendMessageZipWSInputDataType SendMessageZipWSInputData
        {
            get
            {
                return this.sendMessageZipWSInputDataField;
            }
            set
            {
                this.sendMessageZipWSInputDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string BackendSessionId
        {
            get
            {
                return this.backendSessionIdField;
            }
            set
            {
                this.backendSessionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageWithCommentWSInputDataType
    {

        private SendMessageWSInputDataType sendMessageWSInputDataField;

        private string backendSessionIdField;

        private string commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public SendMessageWSInputDataType SendMessageWSInputData
        {
            get
            {
                return this.sendMessageWSInputDataField;
            }
            set
            {
                this.sendMessageWSInputDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string BackendSessionId
        {
            get
            {
                return this.backendSessionIdField;
            }
            set
            {
                this.backendSessionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageZipWithCommentWSInputDataType
    {

        private StoreMessageZipWSInputDataType storeMessageZipWSInputDataField;

        private string backendSessionIdField;

        private string commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StoreMessageZipWSInputDataType StoreMessageZipWSInputData
        {
            get
            {
                return this.storeMessageZipWSInputDataField;
            }
            set
            {
                this.storeMessageZipWSInputDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string BackendSessionId
        {
            get
            {
                return this.backendSessionIdField;
            }
            set
            {
                this.backendSessionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageWithCommentWSInputDataType
    {

        private StoreMessageWSInputDataType storeMessageWSInputDataField;

        private string backendSessionIdField;

        private string commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StoreMessageWSInputDataType StoreMessageWSInputData
        {
            get
            {
                return this.storeMessageWSInputDataField;
            }
            set
            {
                this.storeMessageWSInputDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string BackendSessionId
        {
            get
            {
                return this.backendSessionIdField;
            }
            set
            {
                this.backendSessionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteMessageCommentWSInputDataType
    {

        private string messageIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MessageIdentifier
        {
            get
            {
                return this.messageIdentifierField;
            }
            set
            {
                this.messageIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SetMessageCommentWSInputDataType
    {

        private string messageIdentifierField;

        private string messageCommentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MessageIdentifier
        {
            get
            {
                return this.messageIdentifierField;
            }
            set
            {
                this.messageIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string MessageComment
        {
            get
            {
                return this.messageCommentField;
            }
            set
            {
                this.messageCommentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0")]
    public partial class GuiCommentType
    {

        private string userNameField;

        private System.DateTime commentDateField;

        private string commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public System.DateTime CommentDate
        {
            get
            {
                return this.commentDateField;
            }
            set
            {
                this.commentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessageCommentWSOutputDataType
    {

        private GuiCommentType commentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public GuiCommentType Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessageCommentWSInputDataType
    {

        private string messageIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MessageIdentifier
        {
            get
            {
                return this.messageIdentifierField;
            }
            set
            {
                this.messageIdentifierField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SearchWSInputDataType
    {

        private QueryType searchQueryField;

        private MessageSortByType messagesSortedByField;

        private bool messagesSortedByFieldSpecified;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public QueryType SearchQuery
        {
            get
            {
                return this.searchQueryField;
            }
            set
            {
                this.searchQueryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MessageSortByType MessagesSortedBy
        {
            get
            {
                return this.messagesSortedByField;
            }
            set
            {
                this.messagesSortedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessagesSortedBySpecified
        {
            get
            {
                return this.messagesSortedByFieldSpecified;
            }
            set
            {
                this.messagesSortedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 3)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum MessageSortByType
    {

        /// <remarks/>
        ReceivedSendDateAsc,

        /// <remarks/>
        ReceivedSendDateDesc,

        /// <remarks/>
        SendingMemberStateAsc,

        /// <remarks/>
        SendingMemberStateDesc,

        /// <remarks/>
        ReceivingMemberStateAsc,

        /// <remarks/>
        ReceivingMemberStateDesc,

        /// <remarks/>
        DeadlineDateAsc,

        /// <remarks/>
        DeadlineDateDesc,

        /// <remarks/>
        VersionAsc,

        /// <remarks/>
        VersionDesc,

        /// <remarks/>
        MessageTypeAsc,

        /// <remarks/>
        MessageTypeDesc,

        /// <remarks/>
        DescriptionAsc,

        /// <remarks/>
        DescriptionDesc,

        /// <remarks/>
        HasAttachmentsAsc,

        /// <remarks/>
        HasAttachmentsDesc,

        /// <remarks/>
        FolderAsc,

        /// <remarks/>
        FolderDesc,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ExportReferenceTablesWSOutputDataType
    {

        private ReferenceTableListType referenceTableListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public ReferenceTableListType ReferenceTableList
        {
            get
            {
                return this.referenceTableListField;
            }
            set
            {
                this.referenceTableListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ReferenceTableListType
    {

        private ReferenceTableListTypeReferenceTable referenceTableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public ReferenceTableListTypeReferenceTable ReferenceTable
        {
            get
            {
                return this.referenceTableField;
            }
            set
            {
                this.referenceTableField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ReferenceTableListTypeReferenceTable
    {

        private EntityType[] referenceValueField;

        private string tableNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReferenceValue", Order = 0)]
        public EntityType[] ReferenceValue
        {
            get
            {
                return this.referenceValueField;
            }
            set
            {
                this.referenceValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tableName
        {
            get
            {
                return this.tableNameField;
            }
            set
            {
                this.tableNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/common-reference-tables-v1.0")]
    public abstract partial class EntityType
    {

        private StrictDateType validFromField;

        private StrictDateType validToField;

        private MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] nameField;

        /// <remarks/>
        // CODEGEN Warning: 'default' attribute supported only for primitive types.  Ignoring default='1800-01-01' attribute.
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateType ValidFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        /// <remarks/>
        // CODEGEN Warning: 'default' attribute supported only for primitive types.  Ignoring default='2999-12-31' attribute.
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StrictDateType ValidTo
        {
            get
            {
                return this.validToField;
            }
            set
            {
                this.validToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextLinguisticRepresentation", Namespace = "http://ec.europa.eu/ECRIS-RI/commons-v1.0", IsNullable = false)]
        public MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[] Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ReadNistWSOutputDataType
    {

        private NistBinaryAttachmentType nistAttachmentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public NistBinaryAttachmentType NistAttachment
        {
            get
            {
                return this.nistAttachmentField;
            }
            set
            {
                this.nistAttachmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ReadMessageWSOutputDataType
    {
        [XmlIgnoreAttribute]
        public string SerializedXMLFromService { get; set; }

        private AbstractMessageType ecrisRiMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractMessageType EcrisRiMessage
        {
            get
            {
                return this.ecrisRiMessageField;
            }
            set
            {
                this.ecrisRiMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class EcrisRiIdentifierContainingMessageWSInputDataType
    {

        private string messageIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MessageIdentifier
        {
            get
            {
                return this.messageIdentifierField;
            }
            set
            {
                this.messageIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageWSOutputDataType
    {

        private AbstractMessageType ecrisMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreFolderWSInputDataType
    {

        private FolderType folderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public FolderType Folder
        {
            get
            {
                return this.folderField;
            }
            set
            {
                this.folderField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class RemoveMessagesResult
    {

        private int removedCountField;

        private bool removedCountFieldSpecified;

        private int noAccessToFolderCountField;

        private bool noAccessToFolderCountFieldSpecified;

        private int noRemovalRightsCountField;

        private bool noRemovalRightsCountFieldSpecified;

        private int alreadyDeletedCountField;

        private bool alreadyDeletedCountFieldSpecified;

        private int failedDeadlineCountField;

        private bool failedDeadlineCountFieldSpecified;

        private int failedCountField;

        private bool failedCountFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int RemovedCount
        {
            get
            {
                return this.removedCountField;
            }
            set
            {
                this.removedCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RemovedCountSpecified
        {
            get
            {
                return this.removedCountFieldSpecified;
            }
            set
            {
                this.removedCountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int NoAccessToFolderCount
        {
            get
            {
                return this.noAccessToFolderCountField;
            }
            set
            {
                this.noAccessToFolderCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NoAccessToFolderCountSpecified
        {
            get
            {
                return this.noAccessToFolderCountFieldSpecified;
            }
            set
            {
                this.noAccessToFolderCountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int NoRemovalRightsCount
        {
            get
            {
                return this.noRemovalRightsCountField;
            }
            set
            {
                this.noRemovalRightsCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NoRemovalRightsCountSpecified
        {
            get
            {
                return this.noRemovalRightsCountFieldSpecified;
            }
            set
            {
                this.noRemovalRightsCountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public int AlreadyDeletedCount
        {
            get
            {
                return this.alreadyDeletedCountField;
            }
            set
            {
                this.alreadyDeletedCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AlreadyDeletedCountSpecified
        {
            get
            {
                return this.alreadyDeletedCountFieldSpecified;
            }
            set
            {
                this.alreadyDeletedCountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int FailedDeadlineCount
        {
            get
            {
                return this.failedDeadlineCountField;
            }
            set
            {
                this.failedDeadlineCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FailedDeadlineCountSpecified
        {
            get
            {
                return this.failedDeadlineCountFieldSpecified;
            }
            set
            {
                this.failedDeadlineCountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public int FailedCount
        {
            get
            {
                return this.failedCountField;
            }
            set
            {
                this.failedCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FailedCountSpecified
        {
            get
            {
                return this.failedCountFieldSpecified;
            }
            set
            {
                this.failedCountFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveMessagesWSOutputDataType
    {

        private RemoveMessagesResult removeMessagesResultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public RemoveMessagesResult RemoveMessagesResult
        {
            get
            {
                return this.removeMessagesResultField;
            }
            set
            {
                this.removeMessagesResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveMessagesWSInputDataType
    {

        private string[] messageIdentifierListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MessageIdentifier", IsNullable = false)]
        public string[] MessageIdentifierList
        {
            get
            {
                return this.messageIdentifierListField;
            }
            set
            {
                this.messageIdentifierListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteFolderWSInputDataType
    {

        private string folderIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FolderIdentifier
        {
            get
            {
                return this.folderIdentifierField;
            }
            set
            {
                this.folderIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MoveMessagesToFolderWSInputDataType
    {

        private string folderIdentifierField;

        private string[] messageIdentifierListField;

        private bool ignoreInvalidMessagesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FolderIdentifier
        {
            get
            {
                return this.folderIdentifierField;
            }
            set
            {
                this.folderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MessageIdentifier", IsNullable = false)]
        public string[] MessageIdentifierList
        {
            get
            {
                return this.messageIdentifierListField;
            }
            set
            {
                this.messageIdentifierListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public bool ignoreInvalidMessages
        {
            get
            {
                return this.ignoreInvalidMessagesField;
            }
            set
            {
                this.ignoreInvalidMessagesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessagesForFolderWSOutputDataType
    {

        private MessageShortViewType[] messageShortViewListField;

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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessagesForFolderWSInputDataType
    {

        private string folderIdentifierField;

        private MessageSortByType messagesSortedByField;

        private bool messagesSortedByFieldSpecified;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FolderIdentifier
        {
            get
            {
                return this.folderIdentifierField;
            }
            set
            {
                this.folderIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public MessageSortByType MessagesSortedBy
        {
            get
            {
                return this.messagesSortedByField;
            }
            set
            {
                this.messagesSortedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessagesSortedBySpecified
        {
            get
            {
                return this.messagesSortedByFieldSpecified;
            }
            set
            {
                this.messagesSortedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 3)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindAccessibleMessageByIdentifierWSInputDataType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindMessageByEcrisIdentifierWSInputDataType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageInputDataType
    {

        private string itemField;

        private ItemChoiceType itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcrisIdentifier", typeof(string), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("MessageRiIdentifier", typeof(string), Order = 0)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        EcrisIdentifier,

        /// <remarks/>
        MessageRiIdentifier,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindAccessibleMessageByIdentifierWSInputDataType : FindMessageInputDataType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByEcrisIdentifierWSInputDataType : FindMessageInputDataType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByIdentifierWSOutputDataType
    {

        private MessageShortViewType messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MessageShortViewType Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByIdentifierWSInputDataType
    {

        private string identifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetFoldersWSOutputDataType
    {

        private FolderType[] folderListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Folder", IsNullable = false)]
        public FolderType[] FolderList
        {
            get
            {
                return this.folderListField;
            }
            set
            {
                this.folderListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0")]
    public partial class AuditLogEntryType
    {

        private string usernameField;

        private System.DateTime timestampField;

        private string actionPerformedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Username
        {
            get
            {
                return this.usernameField;
            }
            set
            {
                this.usernameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public System.DateTime Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ActionPerformed
        {
            get
            {
                return this.actionPerformedField;
            }
            set
            {
                this.actionPerformedField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAuditLogWSOutputDataType
    {

        private AuditLogEntryType[] auditLogListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("AuditLogEntry", Namespace = "http://ec.europa.eu/ECRIS-RI/domain-v1.0", IsNullable = false)]
        public AuditLogEntryType[] AuditLogList
        {
            get
            {
                return this.auditLogListField;
            }
            set
            {
                this.auditLogListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAuditLogWSInputDataType
    {

        private StrictDateType auditLogPeriodStartField;

        private StrictDateType auditLogPeriodEndField;

        private int pageNumberField;

        private bool pageNumberFieldSpecified;

        private string itemsPerPageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public StrictDateType AuditLogPeriodStart
        {
            get
            {
                return this.auditLogPeriodStartField;
            }
            set
            {
                this.auditLogPeriodStartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public StrictDateType AuditLogPeriodEnd
        {
            get
            {
                return this.auditLogPeriodEndField;
            }
            set
            {
                this.auditLogPeriodEndField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int PageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PageNumberSpecified
        {
            get
            {
                return this.pageNumberFieldSpecified;
            }
            set
            {
                this.pageNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger", Order = 3)]
        public string ItemsPerPage
        {
            get
            {
                return this.itemsPerPageField;
            }
            set
            {
                this.itemsPerPageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LoginWSOutputDataType
    {

        private UserType authenticatedUserField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public UserType AuthenticatedUser
        {
            get
            {
                return this.authenticatedUserField;
            }
            set
            {
                this.authenticatedUserField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LoginWSInputDataType
    {

        private string loginUserNameField;

        private string loginUserPasswordField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string LoginUserName
        {
            get
            {
                return this.loginUserNameField;
            }
            set
            {
                this.loginUserNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string LoginUserPassword
        {
            get
            {
                return this.loginUserPasswordField;
            }
            set
            {
                this.loginUserPasswordField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class BaseEcrisRiWSOutputDataType
    {

        private string actionExecutionInformationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ActionExecutionInformation
        {
            get
            {
                return this.actionExecutionInformationField;
            }
            set
            {
                this.actionExecutionInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoginWSInputMetaDataType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SessionIdContainingWSMetaDataType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public abstract partial class AbstractWSMetaDataType
    {

        private System.DateTime metaDataTimeStampField;

        private string sessionIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string SessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LoginWSInputMetaDataType : AbstractWSMetaDataType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SessionIdContainingWSMetaDataType : AbstractWSMetaDataType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UpdateEventsStateWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetEventsWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetEventsWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ManageEventTypesConnectionsToUserRolesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MailServerConnectionStatusWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetBuildNumberWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetBuildNumberWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SetSystemMaintenanceModeWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateConfigurationFilesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateConfigurationFilesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UploadConfigurationFilesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UploadConfigurationFilesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PerformFunctionalValidationWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PerformFunctionalValidationWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SetSystemAvailabilityWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ListBackendLogsWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ListBackendLogsWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResetRetransmitCounterWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResetRetransmitCounterWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SystemAvailabilityWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CheckMemberStatesCommunicationsStatusWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CheckMemberStatesCommunicationsStatusWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MoveDownRuleWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MoveUpRuleWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteRuleWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreRuleWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleListWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ListAssignedFolderRolesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ListAssignedFolderRolesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AssignFolderRolesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserListWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleListWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserListWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DisableUserWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EnableUserWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RemoveUserWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PasswordWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RemoveUserRoleWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreRoleWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveAvailableRolesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveAvailableRolesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MemberStateCodeContainingWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveMemberStateEndpointListWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetTransactionForMessageWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetTransactionForMessageWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetYearsMonthsWithStatisticsWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CalculateNextExportDeadlineWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DownloadStatisticsWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DownloadStatisticsWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdministrativeMessageWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveStoredQueriesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveStoredQueryWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteStoredQueriesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SendMessageZipWithCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SendMessageWithCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreMessageZipWithCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreMessageWithCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteMessageCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SetMessageCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetMessageCommentWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetMessageCommentWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExportReferenceTablesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExportReferenceTablesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SendMessageZipWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SendMessageWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SendMessageWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReadNistWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReadMessageWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EcrisRiIdentifierContainingWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreMessageWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreMessageZipWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreMessageWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoreFolderWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RemoveMessagesWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RemoveMessagesWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteFolderWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MoveMessagesToFolderWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetMessagesForFolderWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetMessagesForFolderWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindAccessibleMessageByIdentifierWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindAccessibleMessageByIdentifierWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindMessageByEcrisIdentifierWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindMessageByEcrisIdentifierWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindMessageByIdentifierWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FindMessageByIdentifierWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetFoldersWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveAuditLogWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveAuditLogWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LogoutWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LogoutWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoginWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoginWSInputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseEcrisRiWSOutputType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseEcrisRiWSInputType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public abstract partial class AbstractWSType
    {

        private AbstractWSMetaDataType wSMetaDataField;

        private object wSDataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public AbstractWSMetaDataType WSMetaData
        {
            get
            {
                return this.wSMetaDataField;
            }
            set
            {
                this.wSMetaDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public object WSData
        {
            get
            {
                return this.wSDataField;
            }
            set
            {
                this.wSDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UpdateEventsStateWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetEventsWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetEventsWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ManageEventTypesConnectionsToUserRolesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MailServerConnectionStatusWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetBuildNumberWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetBuildNumberWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SetSystemMaintenanceModeWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ValidateConfigurationFilesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ValidateConfigurationFilesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UploadConfigurationFilesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UploadConfigurationFilesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class PerformFunctionalValidationWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class PerformFunctionalValidationWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SetSystemAvailabilityWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListBackendLogsWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListBackendLogsWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ResetRetransmitCounterWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ResetRetransmitCounterWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SystemAvailabilityWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class CheckMemberStatesCommunicationsStatusWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class CheckMemberStatesCommunicationsStatusWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MoveDownRuleWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MoveUpRuleWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteRuleWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreRuleWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RuleListWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListAssignedFolderRolesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ListAssignedFolderRolesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class AssignFolderRolesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserListWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RuleListWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserListWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DisableUserWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class EnableUserWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveUserWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class PasswordWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class UserWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveUserRoleWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreRoleWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAvailableRolesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAvailableRolesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MemberStateCodeContainingWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveMemberStateEndpointListWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetTransactionForMessageWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetTransactionForMessageWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetYearsMonthsWithStatisticsWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class CalculateNextExportDeadlineWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DownloadStatisticsWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DownloadStatisticsWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class AdministrativeMessageWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveStoredQueriesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveStoredQueryWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteStoredQueriesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SearchWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageZipWithCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageWithCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageZipWithCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageWithCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteMessageCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SetMessageCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessageCommentWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessageCommentWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SearchWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ExportReferenceTablesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ExportReferenceTablesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageZipWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class SendMessageWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ReadNistWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class ReadMessageWSOutputType : AbstractWSType
    {
        
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class EcrisRiIdentifierContainingWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageZipWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreMessageWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class StoreFolderWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveMessagesWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RemoveMessagesWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class DeleteFolderWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class MoveMessagesToFolderWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessagesForFolderWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetMessagesForFolderWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindAccessibleMessageByIdentifierWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindAccessibleMessageByIdentifierWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByEcrisIdentifierWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByEcrisIdentifierWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByIdentifierWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class FindMessageByIdentifierWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class GetFoldersWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAuditLogWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class RetrieveAuditLogWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LogoutWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LogoutWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LoginWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class LoginWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class BaseEcrisRiWSOutputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class BaseEcrisRiWSInputType : AbstractWSType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class OperationFaultType : AbstractFaultType
    {

        private OperationFaultCodeType operationFaultCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public OperationFaultCodeType OperationFaultCode
        {
            get
            {
                return this.operationFaultCodeField;
            }
            set
            {
                this.operationFaultCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public enum OperationFaultCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-001")]
        SDM001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-002")]
        SDM002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-003")]
        SDM003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-004")]
        SDM004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-005")]
        SDM005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-006")]
        SDM006,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-007")]
        SDM007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-001")]
        SM001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-002")]
        SM002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-003")]
        SM003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-004")]
        SM004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-005")]
        SM005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-008")]
        SM008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SM-009")]
        SM009,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("CF-001")]
        CF001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RF-001")]
        RF001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RF-002")]
        RF002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RF-003")]
        RF003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RF-004")]
        RF004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RF-005")]
        RF005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("CU-001")]
        CU001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("CU-002")]
        CU002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RU-001")]
        RU001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RU-002")]
        RU002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("DQ-001")]
        DQ001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SRU-001")]
        SRU001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SRU-002")]
        SRU002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SRO-001")]
        SRO001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RR-001")]
        RR001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RR-002")]
        RR002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RR-003")]
        RR003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-001")]
        MM001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-002")]
        MM002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-003")]
        MM003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-004")]
        MM004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EM-001")]
        EM001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EM-002")]
        EM002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EM-003")]
        EM003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EM-004")]
        EM004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EM-005")]
        EM005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EM-006")]
        EM006,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("DM-001")]
        DM001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RUR-001")]
        RUR001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RUR-002")]
        RUR002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-001")]
        MM0011,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-002")]
        MM0021,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MM-003")]
        MM0031,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("NF-001")]
        NF001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-006")]
        SDM0061,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-007")]
        SDM0071,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-008")]
        SDM008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SDM-009")]
        SDM009,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("KIN-001")]
        KIN001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RM-001")]
        RM001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SC-001")]
        SC001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SC-002")]
        SC002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("CRT-001")]
        CRT001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EV-001")]
        EV001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EV-002")]
        EV002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EV-003")]
        EV003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EV-004")]
        EV004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SE-001")]
        SE001,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/messages-v1.0")]
    public partial class AuthenticationFaultType : AbstractFaultType
    {

        private string authenticationFaultCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string AuthenticationFaultCode
        {
            get
            {
                return this.authenticationFaultCodeField;
            }
            set
            {
                this.authenticationFaultCodeField = value;
            }
        }
    }

}
