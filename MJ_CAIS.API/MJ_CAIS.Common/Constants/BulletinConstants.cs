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
            public const string ConvictionBulletin = "ConvictionBulletin";
            public const string Bulletin78A = "Bulletin78A";
            public const string Unspecified = "Unspecified";
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

            /// <summary>
            /// Глоба
            /// </summary>
            public const string Fine = "nkz_globa";

            /// <summary>
            /// Обществено порицание
            /// </summary>
            public const string PublicDisfavor = "nkz_poricanie";

            /// <summary>
            /// Лишаване от право за заемане на длъжност
            /// </summary>
            public const string DisqualificationPosition = "nkz_lishavane_ot_dlajnost";

            /// <summary>
            /// Лишаване от право за упражняване на професия
            /// </summary>
            public const string DisqualificationProfession = "nkz_lishavane_ot_pravo_profesia";

            /// <summary>
            /// Лишаване от право на местоживеене
            /// </summary>
            public const string DisqualificationPlace = "nkz_lishavane_ot_pravo_mestojiv";

            /// <summary>
            /// Лишаване от право на ордени и др. отличия
            /// </summary>
            public const string DisqualificationMedal = "nkz_lishavane_ot_pravo_nagradi";          
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

            /// <summary>
            /// Постановен съдебен акт по чл. 425 НПК
            /// </summary>
            public const string JudicialAnnulment = "DCH-00-Y";
            
            /// <summary>
            /// Реабилитация
            /// </summary>
            public const string Rehabilitation = "DCH-00-R";
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