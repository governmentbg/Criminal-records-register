using System.Text.Json;
using System.Text.Json.Serialization;

namespace MJ_CAIS.Web.Utils
{
    public class ByteTypeConverter : JsonConverter<byte?>
    {
        private readonly ILogger<ByteTypeConverter> _logger;

        public ByteTypeConverter(ILogger<ByteTypeConverter> logger)
        {
            _logger = logger;
        }

        public override byte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var byteString = reader.GetString();
                // if is empty string return null
                if (string.IsNullOrEmpty(byteString))
                {
                    return null;
                }

                return byte.Parse(byteString);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex,"Cannot get the value of a token type Number as a string.");
                return reader.GetByte();
            }
        }

        public override void Write(Utf8JsonWriter writer, byte? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
