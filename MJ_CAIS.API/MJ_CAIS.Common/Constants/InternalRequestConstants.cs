namespace MJ_CAIS.Common.Constants
{
    public static class InternalRequestConstants
    {
        public static class Status
        {
            public const string Draft = "Draft"; // Чернова
            public const string Sent = "Sent"; // Изпратена
            public const string Cancelled = "Cancelled"; // Отхвърлена
            public const string Ready = "Ready"; // Обработена
            public const string ReadCancelled = "ReadCancelled"; // Отхвърлена от получател и прочетена от изпращача
            public const string ReadReady = "ReadReady"; // Обработена от получател и прочетена от изпращача
        }

        public static class Types
        {
            public const string Rehabilitation = "Rehabilitation"; 
            public const string Technical = "Technical"; 
        }
    }
}