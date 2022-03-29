namespace MJ_CAIS.Common.Constants
{
    public class FbbcConstants
    {
        public static class MessageType
        {
            public const string CodePolice = "1"; // Получени от ЦД "Криминална полиция" 
            public const string CodeCBSHandwritten = "2"; // ЦБС - получени по факс/писмо, ръчно въведени в ЦБС.
            public const string CodeCDKP = "3"; // Данни, получени от ЦД"КП", редактирани в ЦБС. 
            public const string CodeNJR = "4"; // NJR
            public const string CodeECRIS = "5"; // ECRIS
        }
    }
}
