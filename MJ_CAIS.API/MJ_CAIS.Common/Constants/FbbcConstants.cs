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

        public static class MessageTypeDescription
        {
            public const string CodePolice = "От ЦД\"КП\"";
            public const string CodeCBSHandwritten = "ЦБС - ръчно въведени";
            public const string CodeCDKP = "От ЦД\"КП\", редактирани в ЦБС";
            public const string CodeNJR = "NJR";
            public const string CodeECRIS = "ECRIS";
        }

        public static class FBBCStatus
        {
            public const string Active = "Active";
            public const string Deleted = "Deleted";
            public const string ForDelete = "ForDelete";

        }
    }
}
