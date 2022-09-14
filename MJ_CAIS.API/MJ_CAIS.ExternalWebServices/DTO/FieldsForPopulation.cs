using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalWebServices.DTO
{
    public class FieldsForPopulation
    {
       public  FieldsForPopulation() {
            Fields = new List<FieldForPopulation>();
        }
        [JsonInclude]
        [JsonPropertyName("Fields")]
        public List<FieldForPopulation> Fields;
        public void AddFields(FieldsForPopulation valuesToAdd)
        {
            Fields.AddRange(valuesToAdd.Fields);
        }
    }
   
    public class FieldForPopulation
    {
        [JsonInclude]
        [JsonPropertyName("FieldName")]
        public string FieldName;
        [JsonInclude]
        [JsonPropertyName("Priority")]
        public int Priority;
        [JsonInclude]
        [JsonPropertyName("Value")]
        public object Value;
        [DefaultValue ("String")]
        [JsonInclude]
        [JsonPropertyName("ValueType")]
        public string ValueType ;
        [JsonInclude]
        [JsonPropertyName("SourceFieldName")]
        public string SourceFieldName;
        [JsonInclude]
        [DefaultValue("Identity")]
        [JsonPropertyName("TransformationFunctionName")]
        public string TransformationFunctionName ;
        [JsonInclude]
        [JsonPropertyName("IsValueSet")]
        public bool IsValueSet;
        [JsonInclude]
        [JsonPropertyName("IsNavigation")]
        public bool IsNavigation;
        [JsonInclude]
        [JsonPropertyName("FkName")]
        public string FkName;
        [JsonInclude]
        [JsonPropertyName("PropEquality")]
        public string PropEquality;
      
    }
}
