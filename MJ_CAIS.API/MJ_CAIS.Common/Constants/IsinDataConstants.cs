namespace MJ_CAIS.Common.Constants
{
    public static class IsinDataConstants
    {
        public static class Status
        {
            public const string New = "New"; // Новопостъпил и неразпознат
            public const string Identified = "Identified"; // Разпознат
            public const string Unidentified = "Unidentified"; // Невъзможен за разпознаване;
            public const string Closed = "Closed"; // Приключила обработка
        }

        public static class SanctionType
        {
            public const string Fine = "Fine";
            public const string Prison = "Prison"; 
            public const string Probation = "Probation"; 
        }

        public static class SanctionTypeDisplay
        {
            public const string Fine = "Глоба";
            public const string Prison = "Затвор";
            public const string Probation = "Пробация";
        }
    }
}