using MJ_CAIS.DTO.EcrisService;

namespace MJ_CAIS.AutoMapperContainer.Resolvers
{
    public static class AbstractMessageTypeResolver
    {
        public static string GetDateType(DateType date)
        {
            var year = date.DateYear;
            var day = date.DateMonthDay.DateDay.Substring(2);
            var month = date.DateMonthDay.DateMonth.Substring(1);

            return $"{day}/{month}/{year}";
        }
    }
}