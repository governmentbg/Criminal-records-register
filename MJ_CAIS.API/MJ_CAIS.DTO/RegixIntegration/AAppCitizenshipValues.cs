using System.Text.Json.Serialization;

namespace MJ_CAIS.DTO.RegixIntegration
{ 
    public class AAppCitizenshipValues
    {
    [JsonInclude]
    [JsonPropertyName("CountryId")]
    public string CountryId { get;  set; }
    }
}