// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.23276
//    <NameSpace>MJ_CAIS.IntegrationModel</NameSpace><Collection>Array</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace MJ_CAIS.DTO.ExternalServicesHost
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cais.mjs.bg/PersonIdentifierSearchResponse-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute("PersonIdentifierSearchResponse", Namespace="http://cais.mjs.bg/PersonIdentifierSearchResponse-v1.0", IsNullable=false)]
    public partial class PersonIdentifierSearchResponseType {
        
        private PersonIdentifierSearchRequestType reportCriteriaField;
        
        private CriminalRecordsPersonDataType[] reportResultField;
        
        private System.DateTime reportDateField;
        
        /// <summary>
        /// Приложени критерии за търсене
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        [System.ComponentModel.DescriptionAttribute("Приложени критерии за търсене")]
        public PersonIdentifierSearchRequestType ReportCriteria {
            get {
                return this.reportCriteriaField;
            }
            set {
                this.reportCriteriaField = value;
            }
        }
        
        /// <summary>
        /// Резултат от справката
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("PersonData", Namespace="http://cais.mjs.bg/BulletinModel-v1.0", IsNullable=false)]
        [System.ComponentModel.DescriptionAttribute("Резултат от справката")]
        public CriminalRecordsPersonDataType[] ReportResult {
            get {
                return this.reportResultField;
            }
            set {
                this.reportResultField = value;
            }
        }
        
        /// <summary>
        /// Дата на справката
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        [System.ComponentModel.DescriptionAttribute("Дата на справката")]
        public System.DateTime ReportDate {
            get {
                return this.reportDateField;
            }
            set {
                this.reportDateField = value;
            }
        }
    }
}
