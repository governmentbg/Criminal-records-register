namespace MJ_CAIS.Common.Constants
{
    public class PersonConstants
    {
        public const string BG = "CO-00-100-BGR";

        public static class PidType
        {
            /// <summary>
            /// ЕГН
            /// </summary>
            public const string Egn = "EGN";

            /// <summary>
            /// Личен номер на чужденец (ЛНЧ)
            /// </summary>
            public const string Lnch = "LNCH";

            /// <summary>
            /// Личен номер
            /// </summary>
            public const string Ln = "LN";

            /// <summary>
            /// Номер на документ за самоличност
            /// </summary>
            public const string DocumentId = "DOC_ID";

            /// <summary>
            ///  Пръстов отпечатък
            /// </summary>
            public const string AfisNumber = "AFIS";

            /// <summary>
            ///  Вътрешен системен идентификатор (добавя се при миграция на данни)
            /// </summary>
            public const string Suid = "SYS";
        }

        public static class IssuerType
        {
            /// <summary>
            /// В случай на ЛН
            /// </summary>
            public const string EU = "EU";

            /// <summary>
            /// В случай на ЕГН
            /// </summary>
            public const string GRAO = "GRAO";

            /// <summary>
            /// В случай на  ЛНЧ или АФИС
            /// </summary>
            public const string MVR = "MVR";

            /// <summary>
            /// В случай на системно генериран идентификатор от ЦАИС
            /// </summary>
            public const string CRR = "CRR";
        }

        public static class ContextTable
        {
            public const string Bulletins = "B_BULLETINS";
            public const string Application = "A_APPLICATIONS";
            public const string WApplication = "W_APPLICATIONS";
            public const string Report = "A_REPORTS";
            public const string Fbbc = "FBBC";
            public const string Manual = "MANUAL";
        }

        public static class SexType
        {
            public const decimal Male = 1;
            public const decimal Female = 2;         
        }
    }
}