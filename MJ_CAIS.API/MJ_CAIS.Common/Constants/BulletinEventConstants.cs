namespace MJ_CAIS.Common.Constants
{
    public static class BulletinEventConstants
    {
        public static class Type
        {
            /// <summary>
            /// по чл.22, чл. 1, изр. 1
            /// </summary>
            public const string Article2211 = "Article2211";

            /// <summary>
            /// по чл.22, чл. 1, изр. 2
            /// </summary>
            public const string Article2212 = "Article2212";

            /// <summary>
            /// по чл.30
            /// </summary>
            public const string Article3000 = "Article3000";

            /// <summary>
            /// добавен документ
            /// </summary>
            public const string NewDocument = "NewDocument";
        }

        public static class Status
        {
            public const string New = "New";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
        }
    }
}
