using NodaTime;
using NodaTime.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MJ_CAIS.Web.Utils
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private static DateTimeZone SOFIA => DateTimeZoneProviders.Tzdb["Europe/Sofia"];
        private readonly ILogger<DateTimeConverter> _logger;

        public DateTimeConverter(ILogger<DateTimeConverter> logger)
        {
            _logger = logger;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // NodaTime се използва поради ето този проблем: 
            // https://docs.microsoft.com/en-us/dotnet/api/system.timezone.tolocaltime?view=net-6.0
            // .net-а бърка daylight save-овете за исторически дати.
            var dateString = reader.GetString();
            return ParseDate(dateString, _logger);
        }

        public static DateTime ParseDate(string? dateString, ILogger logger)
        {
            var parsedDate = InstantPattern.ExtendedIso.Parse(dateString);
            if (parsedDate.Success)
            {
                var localDate = parsedDate.Value.InZone(SOFIA).LocalDateTime.ToDateTimeUnspecified();
                var date = DateTime.SpecifyKind(localDate, DateTimeKind.Local);
                return date;
            }
            else
            {
                var parsedOffsetDate = OffsetDateTimePattern.ExtendedIso.Parse(dateString);
                if (parsedOffsetDate.Success)
                {
                    var localDate = parsedOffsetDate.Value.InZone(SOFIA).LocalDateTime.ToDateTimeUnspecified();
                    var date = DateTime.SpecifyKind(localDate, DateTimeKind.Local);
                    return date;
                }
                else
                {
                    logger.LogWarning("Instant pattern and Offset pattern failed to parse date: {dateString}. Trying Date.Parse...", dateString);
                    var date = DateTime.Parse(dateString);
                    var localDate = date.ToLocalTime();
                    return localDate;
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var ldt = new LocalDateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            var date = SOFIA.AtStrictly(ldt);
            writer.WriteStringValue(date.ToDateTimeOffset().ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}
