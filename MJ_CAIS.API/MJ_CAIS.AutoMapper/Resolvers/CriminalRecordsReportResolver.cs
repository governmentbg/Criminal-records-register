using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static MJ_CAIS.Common.Constants.GlobalConstants;

namespace MJ_CAIS.AutoMapperContainer.Resolvers
{
    public static class CriminalRecordsReportResolver
    {
        public static string DurationYearPattern = "P(\\d+)Y";
        public static string DurationMonthPattern = "Y(\\d+)M";
        public static string DurationDayPattern = "M(\\d+)D";
        public static string DurationHourPattern = "T(\\d+)H";

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
            if(!string.IsNullOrEmpty(datePrecision))
            {
                result.DatePrecision = Enum.Parse<DatePrecisionEnum>(datePrecision);
            }
            result.DatePrecisionSpecified = string.IsNullOrEmpty(datePrecision);
                        
            return result;
        }

        public static DateTime? GetDateTimeFromDateType(DateType dateType)
        {
            var isYearParsed = int.TryParse(dateType.DateYear,  out int yearParsed);
            var isMonthParsed = int.TryParse(dateType.DateMonthDay.DateMonth,  out int monthParsed);
            var isDayParsed = int.TryParse(dateType.DateMonthDay.DateDay,  out int daysParsed);

            if (!isYearParsed && !isMonthParsed && !isDayParsed) return null;

            var year = isYearParsed ? yearParsed : new DateTime().Year;
            var month = isMonthParsed ? monthParsed : 1;
            var day = isDayParsed ? daysParsed : 1;

            return new DateTime(year, month, day);          
        }

        public static string GetDatePrecFromDateType(DateType dateType)
        {
            var isFulleDate = !string.IsNullOrEmpty(dateType.DateYear) &&
                    dateType.DateMonthDay != null &&
                    !string.IsNullOrEmpty(dateType.DateMonthDay.DateMonth) &&
                    !string.IsNullOrEmpty(dateType.DateMonthDay.DateDay);

            if (isFulleDate) return DatePrecisionType.YMD;

            var isYearAndMonth = !string.IsNullOrEmpty(dateType.DateYear) &&
                    dateType.DateMonthDay != null &&
                    !string.IsNullOrEmpty(dateType.DateMonthDay.DateMonth);

            if (isYearAndMonth) return DatePrecisionType.YM;

            return DatePrecisionType.Y;          
        }

        public static string GetPeriodFromNumbers(int? years, int? months, int? days, int? hours)
        {
            return "P" +
            ((years == null || years == 0) ? "0Y" : (years.ToString() + "Y"))
            + ((months == null || months == 0) ? "0M" : (months.ToString() + "M"))
            + ((days == null || days == 0) ? "0D" : (days.ToString() + "D"))
            + ((hours == null || hours == 0) ? "T0H" : ("T" + hours.ToString() + "H"));
        }

        public static int? GetDurationPart(string? duration, string pattern)
        {
            if(string.IsNullOrEmpty(duration)) return null;

            var match = new Regex(pattern).Match(duration);
            if (!match.Success || match.Groups.Count == 0) return null; 
         
            var isParsedPart = int.TryParse(match.Groups[0].Value, out int part);

            return isParsedPart ? part : null;
        }
      
        public static PersonIdentityNumberType GetPersonPids(IEnumerable<PPersonId> pids)
        {
            var result = new PersonIdentityNumberType();
            result.EGN = pids.FirstOrDefault(x => x.PidType.Code == PersonConstants.PidType.Egn)?.Pid;
            result.LNCh = pids.FirstOrDefault(x => x.PidType.Code == PersonConstants.PidType.Lnch)?.Pid;
            result.LN = pids.FirstOrDefault(x => x.PidType.Code == PersonConstants.PidType.Ln)?.Pid;
            result.SUID = pids.FirstOrDefault(x => x.PidType.Code == PersonConstants.PidType.Suid)?.Pid;

            return result;
        }

        public static TEnumType StringToEnum<TEnumType>(string stringValue)
            where TEnumType : struct, IConvertible
        {
            if (string.IsNullOrEmpty(stringValue)) return default;

            if (!typeof(TEnumType).IsEnum)
            {
                throw new ArgumentException("TEnumType must be an enumerated type");
            }

            // когато са само цифри Enum.TryParse успява да парсне стойността от атрибута защото е число
            //[XmlEnumAttribute("50")]
            //Item50,
            if (stringValue.All(x => char.IsDigit(x)))
            {
                return GetEnumValueFromXmlAttributeName<TEnumType>(stringValue);
            }

            // когато няма XmlEnumAttribute за избраната стойност от енума
            if (Enum.TryParse(stringValue, true, out TEnumType result))
            {
                return result;
            }

            // когато стойността е от XmlEnumAttribute и не е число
            //[XmlEnumAttribute("SHT-SBT")]
            //SHTSBT,
            var enumFromXml = GetEnumValueFromXmlAttributeName<TEnumType>(stringValue);
            return enumFromXml;
        }

        public static string EnumToString<TEnumValue>(TEnumValue enumValue)
          where TEnumValue : struct, IConvertible
        {
            if (!typeof(TEnumValue).IsEnum)
            {
                throw new ArgumentException($"{typeof(TEnumValue)} must be an enumerated type");
            }

            var currentEnum = enumValue as Enum;
            var enumAsString = enumValue.ToString();
            var textFromAttr = currentEnum.GetXmlEnumName();
            return !string.IsNullOrEmpty(textFromAttr) ? textFromAttr : enumAsString;
        }

        private static string GetXmlEnumName(this Enum value)
        {
            var attribute = value.GetAttribute<XmlEnumAttribute>();
            return attribute == null ? value.ToString() : attribute.Name;
        }

        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
              ? (T)attributes[0]
              : null;
        }

        private static TEnumType GetEnumValueFromXmlAttributeName<TEnumType>(string attribVal)
          where TEnumType : struct, IConvertible
        {
            var fields = typeof(TEnumType).GetFields();

            foreach (var field in fields)
            {
                var attribs = field.GetCustomAttributes(typeof(XmlEnumAttribute), false);
                foreach (object attr in attribs)
                {
                    if ((attr as XmlEnumAttribute).Name.Equals(attribVal))
                    {
                        return (TEnumType)field.GetValue(null);
                    }
                }
            }

            return default;
        }
    }
}
