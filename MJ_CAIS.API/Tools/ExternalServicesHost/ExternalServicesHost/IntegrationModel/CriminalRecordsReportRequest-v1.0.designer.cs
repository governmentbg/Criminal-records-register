// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.23276
//    <NameSpace>MJ_CAIS.IntegrationModel</NameSpace><Collection>Array</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace MJ_CAIS.IntegrationModel {
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.23297")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://cais.mjs.bg/CriminalRecordsReportRequest-v1.0")]
    [System.Xml.Serialization.XmlRootAttribute("CriminalRecordsExtendedRequest", Namespace="http://cais.mjs.bg/CriminalRecordsReportRequest-v1.0", IsNullable=false)]
    public partial class CriminalRecordsExtendedRequestType {
        
        private CriminalRecordsRequestType criminalRecordsRequestField;
        
        private CallContext callContextField;
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public CriminalRecordsRequestType CriminalRecordsRequest {
            get {
                return this.criminalRecordsRequestField;
            }
            set {
                this.criminalRecordsRequestField = value;
            }
        }
        
        /// <summary>
        /// Служебна информация
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        [System.ComponentModel.DescriptionAttribute("Служебна информация")]
        public CallContext CallContext {
            get {
                return this.callContextField;
            }
            set {
                this.callContextField = value;
            }
        }
    }
}
