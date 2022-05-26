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


        public static class SanctionType
        {
            /// <summary>
            /// Лишаване от свобода
            /// </summary>
            public const string Imprisonment = "nkz_lishavane_ot_svoboda";

            /// <summary>
            /// Пробация
            /// </summary>
            public const string Probation = "nkz_probacia";

            /// <summary>
            /// Доживотен затвор
            /// </summary>
            public const string LifeImprisonment = "nkz_dojiv_zatvor";

            /// <summary>
            /// Доживотен затвор без замяна
            /// </summary>
            public const string LifeImprisonmentWithoutParole = "nkz_dojiv_zatvor_bez_zamiana";
        }

        public static class DecisionType
        {
            /// <summary>
            /// Край на изтърпяването на наказанието
            /// </summary>
            public const string EndOfPenalty = "DCH-00-N";

            /// <summary>
            /// Помилване
            /// </summary>
            public const string Pardon = "DCH-00-O";         
        }
      
        public static class CaseType
        {
            /// <summary>
            /// Наказателно дело от общ характер
            /// </summary>
            public const string NOXD = "sign_noxd";
        }
    }
}