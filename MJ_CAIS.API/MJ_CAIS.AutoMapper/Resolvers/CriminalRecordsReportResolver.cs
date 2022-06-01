using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using System.Xml.Serialization;
using static MJ_CAIS.Common.Constants.GlobalConstants;

namespace MJ_CAIS.AutoMapperContainer.Resolvers
{
    public static class CriminalRecordsReportResolver
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
            if(!string.IsNullOrEmpty(datePrecision))
            {
                result.DatePrecision = Enum.Parse<DatePrecisionEnum>(datePrecision);
            }
            result.DatePrecisionSpecified = string.IsNullOrEmpty(datePrecision);
                        
            return result;
        }

        public static string GetPeriodFromNumbers(int? years, int? months, int? days, int? hours)
        {
            return "P" +
            ((years == null || years == 0) ? "0Y" : (years.ToString() + "Y"))
            + ((months == null || months == 0) ? "0M" : (months.ToString() + "M"))
            + ((days == null || days == 0) ? "0D" : (days.ToString() + "D"))
            + ((hours == null || hours == 0) ? "T0H" : ("T" + hours.ToString() + "H"));
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
