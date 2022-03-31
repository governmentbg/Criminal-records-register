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
    }
}