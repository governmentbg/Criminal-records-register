// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.24803
//    <NameSpace>TechnoLogica.RegiX.NRAObligatedPersonsAdapter</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace TechnoLogica.RegiX.NRAObligatedPersonsAdapter {
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.26028")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://egov.bg/RegiX/NRA/SocialSecurityDataFromDeclarations/Request")]
    [System.Xml.Serialization.XmlRootAttribute("GetSocialSecurityDataFromDeclarationsRequest", Namespace="http://egov.bg/RegiX/NRA/SocialSecurityDataFromDeclarations/Request", IsNullable=false)]
    public partial class SocialSecurityDataFromDeclarationRequestType {
        
        private string personIdentifierField;
        
        private PersonIdentifierTypeEnumeration personIdentifierTypeField;
        
        private bool personIdentifierTypeFieldSpecified;
        
        private string insurerIdentificatorField;
        
        private string monthFromField;
        
        private string yearFromField;
        
        private string monthToField;
        
        private string yearToField;
        
        /// <summary>
        /// Идентификатор за осигуреното физическо лице – с ограничение до 10 разряда
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        [System.ComponentModel.DescriptionAttribute("Идентификатор за осигуреното физическо лице – с ограничение до 10 разряда")]
        public string PersonIdentifier {
            get {
                return this.personIdentifierField;
            }
            set {
                this.personIdentifierField = value;
            }
        }
        
        /// <summary>
        /// Тип на идентификатор за физическите лица - с възможност за избор на:
        /// •	ЕГН;
        /// •	ЛН/ЛНЧ;
        /// •	Служебен номер от регистъра на НАП;
        /// •	ЕИК по БУЛСТАТ.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        [System.ComponentModel.DescriptionAttribute("Тип на идентификатор за физическите лица - с възможност за избор на: \n•\tЕГН; \n•\tЛ" +
            "Н/ЛНЧ; \n•\tСлужебен номер от регистъра на НАП; \n•\tЕИК по БУЛСТАТ.")]
        public PersonIdentifierTypeEnumeration PersonIdentifierType {
            get {
                return this.personIdentifierTypeField;
            }
            set {
                this.personIdentifierTypeField = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PersonIdentifierTypeSpecified {
            get {
                return this.personIdentifierTypeFieldSpecified;
            }
            set {
                this.personIdentifierTypeFieldSpecified = value;
            }
        }
        
        /// <summary>
        /// Идентификатор на осигурителя (ЕИК/сл.№ от регистъра на НАП) -  с ограничение до 13 разряда.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        [System.ComponentModel.DescriptionAttribute("Идентификатор на осигурителя (ЕИК/сл.№ от регистъра на НАП) -  с ограничение до 1" +
            "3 разряда.")]
        public string InsurerIdentificator {
            get {
                return this.insurerIdentificatorField;
            }
            set {
                this.insurerIdentificatorField = value;
            }
        }
        
        /// <summary>
        /// Месец от
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="gMonth", Order=3)]
        [System.ComponentModel.DescriptionAttribute("Месец от")]
        public string MonthFrom {
            get {
                return this.monthFromField;
            }
            set {
                this.monthFromField = value;
            }
        }
        
        /// <summary>
        /// Година от
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="gYear", Order=4)]
        [System.ComponentModel.DescriptionAttribute("Година от")]
        public string YearFrom {
            get {
                return this.yearFromField;
            }
            set {
                this.yearFromField = value;
            }
        }
        
        /// <summary>
        /// Месец до
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="gMonth", Order=5)]
        [System.ComponentModel.DescriptionAttribute("Месец до")]
        public string MonthTo {
            get {
                return this.monthToField;
            }
            set {
                this.monthToField = value;
            }
        }
        
        /// <summary>
        /// Година до
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="gYear", Order=6)]
        [System.ComponentModel.DescriptionAttribute("Година до")]
        public string YearTo {
            get {
                return this.yearToField;
            }
            set {
                this.yearToField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.26028")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://egov.bg/RegiX/NRA/SocialSecurityDataFromDeclarations/Request")]
    public enum PersonIdentifierTypeEnumeration {
        
        /// <remarks/>
        NOT_SPECIFIED,
        
        /// <remarks/>
        EGN,
        
        /// <remarks/>
        LNCh,
        
        /// <remarks/>
        NRASystemNo,
        
        /// <remarks/>
        EIK_BULSTAT,
    }
}
