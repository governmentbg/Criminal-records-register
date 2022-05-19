namespace MJ_CAIS.Common.Constants
{
    public class BulletinConstants
    {
        public static class Status
        {
            public const string NewEISS = "NewEISS"; // Нов зареден от ЕИСС
            public const string NewOffice = "NewOffice"; //Нов въведен от служител на БС
            public const string Active = "Active"; //	Актуален
            public const string ForDestruction = "ForDestruction"; //Подлежащ на унищожаване
            public const string Deleted = "Deleted"; //	Унищожен
            public const string ForRehabilitation = "ForRehabilitation"; //	Подлежащ на реабилитация на лицето
            public const string Rehabilitated = "Rehabilitated"; //	Извършена реабилитация
            public const string ReplacedAct425 = "ReplacedAct425"; // Постановен съдебен акт по чл. 425 НПК
            public const string NoSanction = "NoSanction"; // Без наказание
        }

        public static class Type
        {
            public const string ConvictionBulletin = "за съдимост";
            public const string Bulletin78A = "по чл.78а";
            public const string Unspecified = "неопределен";
        }
    }
}