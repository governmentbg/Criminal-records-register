// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.23276
//    <NameSpace>MJ_CAIS.DTO.ExternalServicesHost</NameSpace><Collection>Array</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace MJ_CAIS.DTO.ExternalServicesHost {
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute("SendFineDataRequest", Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0", IsNullable=false)]
    public partial class SendFineDataRequestType {
        
        private FineDataListType fineDataListField;
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public FineDataListType FineDataList {
            get {
                return this.fineDataListField;
            }
            set {
                this.fineDataListField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0", IsNullable=true)]
    public partial class FineDataListType {
        
        private FineData[] fineField;
        
        [System.Xml.Serialization.XmlElementAttribute("Fine", Order=0)]
        public FineData[] Fine {
            get {
                return this.fineField;
            }
            set {
                this.fineField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0", IsNullable=true)]
    public partial class FineData {
        
        private PersonDataType personDataField;
        
        private ConvictionDataType convictionDataField;
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public PersonDataType PersonData {
            get {
                return this.personDataField;
            }
            set {
                this.personDataField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ConvictionDataType ConvictionData {
            get {
                return this.convictionDataField;
            }
            set {
                this.convictionDataField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0", IsNullable=true)]
    public partial class PersonDataType {
        
        private string identifierTypeField;
        
        private string identifierField;
        
        private string firstNameField;
        
        private string surNameField;
        
        private string familyNameField;
        
        private System.DateTime birthDateField;
        
        private string sexField;
        
        private string countryCode1Field;
        
        private string countryName1Field;
        
        private string countryCode2Field;
        
        private string countryName2Field;
        
        private string birthCountryCodeField;
        
        private string birthCountryNameField;
        
        private string birthPlaceField;
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string IdentifierType {
            get {
                return this.identifierTypeField;
            }
            set {
                this.identifierTypeField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Identifier {
            get {
                return this.identifierField;
            }
            set {
                this.identifierField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SurName {
            get {
                return this.surNameField;
            }
            set {
                this.surNameField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string FamilyName {
            get {
                return this.familyNameField;
            }
            set {
                this.familyNameField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=5)]
        public System.DateTime BirthDate {
            get {
                return this.birthDateField;
            }
            set {
                this.birthDateField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Sex {
            get {
                return this.sexField;
            }
            set {
                this.sexField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string CountryCode1 {
            get {
                return this.countryCode1Field;
            }
            set {
                this.countryCode1Field = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string CountryName1 {
            get {
                return this.countryName1Field;
            }
            set {
                this.countryName1Field = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string CountryCode2 {
            get {
                return this.countryCode2Field;
            }
            set {
                this.countryCode2Field = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string CountryName2 {
            get {
                return this.countryName2Field;
            }
            set {
                this.countryName2Field = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string BirthCountryCode {
            get {
                return this.birthCountryCodeField;
            }
            set {
                this.birthCountryCodeField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string BirthCountryName {
            get {
                return this.birthCountryNameField;
            }
            set {
                this.birthCountryNameField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string BirthPlace {
            get {
                return this.birthPlaceField;
            }
            set {
                this.birthPlaceField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute("SendBulletinsDataRequest", Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0", IsNullable=false)]
    public partial class SendBulletinsDataRequestType {
        
        private BulletinsList bulletinsListField;
        
        /// <summary>
        /// Списък с бюлетини
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        [System.ComponentModel.DescriptionAttribute("Списък с бюлетини")]
        public BulletinsList BulletinsList {
            get {
                return this.bulletinsListField;
            }
            set {
                this.bulletinsListField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://cs.mjs.bg/EISSServicesModel-v1.0", IsNullable=true)]
    public partial class ConvictionDataType {
        
        private string actTypeCodeField;
        
        private string actNumberField;
        
        private System.DateTime actDateField;
        
        private bool actDateFieldSpecified;
        
        private System.DateTime actFinalDateField;
        
        private bool actFinalDateFieldSpecified;
        
        private string actDecidingAuthorityCodeField;
        
        private string actDecidingAuthorityNameField;
        
        private string caseNumberField;
        
        private string caseYearField;
        
        private string caseTypeCodeField;
        
        private string caseDecidingAuthorityCodeField;
        
        private string caseDecidingAuthorityNameField;
        
        private string legalProvisionsField;
        
        private string remarksField;
        
        private System.DateTime excecutionEndDateField;
        
        private bool excecutionEndDateFieldSpecified;
        
        private object sanctionTypeField;
        
        /// <summary>
        /// Вид на акта
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        [System.ComponentModel.DescriptionAttribute("Вид на акта")]
        public string ActTypeCode {
            get {
                return this.actTypeCodeField;
            }
            set {
                this.actTypeCodeField = value;
            }
        }
        
        /// <summary>
        /// Номер на акта
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        [System.ComponentModel.DescriptionAttribute("Номер на акта")]
        public string ActNumber {
            get {
                return this.actNumberField;
            }
            set {
                this.actNumberField = value;
            }
        }
        
        /// <summary>
        /// Дата на акта
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=2)]
        [System.ComponentModel.DescriptionAttribute("Дата на акта")]
        public System.DateTime ActDate {
            get {
                return this.actDateField;
            }
            set {
                this.actDateField = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActDateSpecified {
            get {
                return this.actDateFieldSpecified;
            }
            set {
                this.actDateFieldSpecified = value;
            }
        }
        
        /// <summary>
        /// Влиза в сила
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=3)]
        [System.ComponentModel.DescriptionAttribute("Влиза в сила")]
        public System.DateTime ActFinalDate {
            get {
                return this.actFinalDateField;
            }
            set {
                this.actFinalDateField = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActFinalDateSpecified {
            get {
                return this.actFinalDateFieldSpecified;
            }
            set {
                this.actFinalDateFieldSpecified = value;
            }
        }
        
        /// <summary>
        /// Код на съд, издал акта
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        [System.ComponentModel.DescriptionAttribute("Код на съд, издал акта")]
        public string ActDecidingAuthorityCode {
            get {
                return this.actDecidingAuthorityCodeField;
            }
            set {
                this.actDecidingAuthorityCodeField = value;
            }
        }
        
        /// <summary>
        /// Наименование на съд, издал акта
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        [System.ComponentModel.DescriptionAttribute("Наименование на съд, издал акта")]
        public string ActDecidingAuthorityName {
            get {
                return this.actDecidingAuthorityNameField;
            }
            set {
                this.actDecidingAuthorityNameField = value;
            }
        }
        
        /// <summary>
        /// дело номер
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        [System.ComponentModel.DescriptionAttribute("дело номер")]
        public string CaseNumber {
            get {
                return this.caseNumberField;
            }
            set {
                this.caseNumberField = value;
            }
        }
        
        /// <summary>
        /// дело година
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="gYear", Order=7)]
        [System.ComponentModel.DescriptionAttribute("дело година")]
        public string CaseYear {
            get {
                return this.caseYearField;
            }
            set {
                this.caseYearField = value;
            }
        }
        
        /// <summary>
        /// вид
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        [System.ComponentModel.DescriptionAttribute("вид")]
        public string CaseTypeCode {
            get {
                return this.caseTypeCodeField;
            }
            set {
                this.caseTypeCodeField = value;
            }
        }
        
        /// <summary>
        /// Код на съд на дело
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        [System.ComponentModel.DescriptionAttribute("Код на съд на дело")]
        public string CaseDecidingAuthorityCode {
            get {
                return this.caseDecidingAuthorityCodeField;
            }
            set {
                this.caseDecidingAuthorityCodeField = value;
            }
        }
        
        /// <summary>
        /// Наименование на съд на дело
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        [System.ComponentModel.DescriptionAttribute("Наименование на съд на дело")]
        public string CaseDecidingAuthorityName {
            get {
                return this.caseDecidingAuthorityNameField;
            }
            set {
                this.caseDecidingAuthorityNameField = value;
            }
        }
        
        /// <summary>
        /// Правно основание(по чл.)
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        [System.ComponentModel.DescriptionAttribute("Правно основание(по чл.)")]
        public string LegalProvisions {
            get {
                return this.legalProvisionsField;
            }
            set {
                this.legalProvisionsField = value;
            }
        }
        
        /// <summary>
        /// Описание и бележки
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        [System.ComponentModel.DescriptionAttribute("Описание и бележки")]
        public string Remarks {
            get {
                return this.remarksField;
            }
            set {
                this.remarksField = value;
            }
        }
        
        /// <summary>
        /// Дата, на която е платена глобата
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        [System.ComponentModel.DescriptionAttribute("Дата, на която е платена глобата")]
        public System.DateTime ExcecutionEndDate {
            get {
                return this.excecutionEndDateField;
            }
            set {
                this.excecutionEndDateField = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExcecutionEndDateSpecified {
            get {
                return this.excecutionEndDateFieldSpecified;
            }
            set {
                this.excecutionEndDateFieldSpecified = value;
            }
        }
        
        /// <summary>
        /// Fine, Prison,Probation
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        [System.ComponentModel.DescriptionAttribute("Fine, Prison,Probation")]
        public object SanctionType {
            get {
                return this.sanctionTypeField;
            }
            set {
                this.sanctionTypeField = value;
            }
        }
    }
}
