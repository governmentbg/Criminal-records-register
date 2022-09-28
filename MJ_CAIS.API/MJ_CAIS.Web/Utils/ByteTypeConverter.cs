using System.Text.Json;
using System.Text.Json.Serialization;

namespace MJ_CAIS.Web.Utils
{
    public class ByteTypeConverter : JsonConverter<byte?>
    {
        public override byte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var byteString = reader.GetString();
            // input nuber in transaction sent empty string not null
            if (string.IsNullOrEmpty(byteString))
            {
                return null;
            }

            return byte.Parse(byteString);
        }

        public override void Write(Utf8JsonWriter writer, byte? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
