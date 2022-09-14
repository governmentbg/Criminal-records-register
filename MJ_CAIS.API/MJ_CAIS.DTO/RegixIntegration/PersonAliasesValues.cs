using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.RegixIntegration
{
    public class PersonAliasesValues
    {
        [JsonInclude]
        [JsonPropertyName("Firstname")]
        public string? Firstname { get; set; }
        [JsonInclude]
        [JsonPropertyName("Surname")]
        public string? Surname { get; set; }
        [JsonInclude]
        [JsonPropertyName("Familyname")]
        public string? Familyname { get; set; }
        [JsonInclude]
        [JsonPropertyName("Fullname")]
        public string? Fullname { get; set; }
        [JsonInclude]
        [JsonPropertyName("Type")]
        public string? Type { get; set; }
    }
}
