using MJ_CAIS.DTO.ExternalServicesHost;
using static MJ_CAIS.Common.Constants.GlobalConstants;

namespace MJ_CAIS.AutoMapperContainer.Resolvers
{
    public static class AbstractMessageTypeProfile
    {
        public static DateType GetDateType(DateTime? date, string datePrecision)
        {
            if (date == null) return null;
            if (string.IsNullOrEmpty(datePrecision))
            {
                datePrecision = DatePrecisionType.YMD;
            }

            var dateValue = date.Value;



            var result = new DateType();
            result.Date = dateValue;
            if (!string.IsNullOrEmpty(datePrecision))
            {
                result.DatePrecision = Enum.Parse<DatePrecisionEnum>(datePrecision.ToUpper());
            }
            result.DatePrecisionSpecified = string.IsNullOrEmpty(datePrecision);

            return result;
        }
    }
}
