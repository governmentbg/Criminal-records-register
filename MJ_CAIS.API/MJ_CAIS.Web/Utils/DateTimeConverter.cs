using System.Text.Json;
using System.Text.Json.Serialization;

namespace MJ_CAIS.Web.Utils
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var date = DateTime.Parse(reader.GetString());
            var localDate = date.ToLocalTime();
            return date;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var date = value.ToUniversalTime();
            writer.WriteStringValue(date);
        }
    }
}
