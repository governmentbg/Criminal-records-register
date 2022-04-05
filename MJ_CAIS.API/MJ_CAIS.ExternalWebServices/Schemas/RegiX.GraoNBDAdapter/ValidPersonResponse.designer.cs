// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.24467
//    <NameSpace>TechnoLogica.RegiX.GraoNBDAdapter</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace TechnoLogica.RegiX.GraoNBDAdapter {
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.26174")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://egov.bg/RegiX/GRAO/NBD/ValidPersonResponse")]
    [System.Xml.Serialization.XmlRootAttribute("ValidPersonResponse", Namespace="http://egov.bg/RegiX/GRAO/NBD/ValidPersonResponse", IsNullable=false)]
    public partial class ValidPersonResponseType {
        
        private string firstNameField;
        
        private string surNameField;
        
        private string familyNameField;
        
        private System.DateTime birthDateField;
        
        private bool birthDateFieldSpecified;
        
        private System.DateTime deathDateField;
        
        private bool deathDateFieldSpecified;
        
        /// <summary>
        /// Собствено име
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        [System.ComponentModel.DescriptionAttribute("Собствено име")]
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <summary>
        /// Бащино име
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        [System.ComponentModel.DescriptionAttribute("Бащино име")]
        public string SurName {
            get {
                return this.surNameField;
            }
            set {
                this.surNameField = value;
            }
        }
        
        /// <summary>
        /// Фамилно име
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        [System.ComponentModel.DescriptionAttribute("Фамилно име")]
        public string FamilyName {
            get {
                return this.familyNameField;
            }
            set {
                this.familyNameField = value;
            }
        }
        
        /// <summary>
        /// Дата на раждане
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=3)]
        [System.ComponentModel.DescriptionAttribute("Дата на раждане")]
        public System.DateTime BirthDate {
            get {
                return this.birthDateField;
            }
            set {
                this.birthDateField = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BirthDateSpecified {
            get {
                return this.birthDateFieldSpecified;
            }
            set {
                this.birthDateFieldSpecified = value;
            }
        }
        
        /// <summary>
        /// Дата на смърт
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=4)]
        [System.ComponentModel.DescriptionAttribute("Дата на смърт")]
        public System.DateTime DeathDate {
            get {
                return this.deathDateField;
            }
            set {
                this.deathDateField = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeathDateSpecified {
            get {
                return this.deathDateFieldSpecified;
            }
            set {
                this.deathDateFieldSpecified = value;
            }
        }
    }
}
