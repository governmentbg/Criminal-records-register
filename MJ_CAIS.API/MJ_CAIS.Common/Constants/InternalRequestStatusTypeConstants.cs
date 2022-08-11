namespace MJ_CAIS.Common.Constants
{
    public class InternalRequestStatusTypeConstants
    {
        public const string Draft = "Draft"; // Чернова
        public const string Sent = "Sent"; // Изпратена
        public const string Cancelled = "Cancelled"; // Отхвърлена
        public const string Ready = "Ready"; // Обработена
        public const string ReadCancelled = "ReadCancelled"; // Отхвърлена от получател и прочетена от изпращача
        public const string ReadReady = "ReadReady"; // Обработена от получател и прочетена от изпращача
    }
}