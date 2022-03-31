namespace MJ_CAIS.Common.Constants
{
    public class BulletinConstants
    {
        // todo: да се организират по-добре всички константи? като енъми?
        public static class Status
        {
            public const string NewEISS = "NewEISS"; // Нов зареден от ЕИСС
            public const string NewOffice = "NewOffice"; //Нов въведен от служител на БС
            public const string ExpectPaper = "ExpectPaper"; // Очакващ потвърждение за получаване на хартиено копие в БС по месторождение
            public const string ReceivedPaper = "ReceivedPaper"; // Потвърдено получаване в БС по месторождение
            public const string ForEcris = "ForEcris"; //За изпращане на нотификация към ECRIS-RI
            public const string ForEcrisTCN = "ForEcrisTCN"; //За изпращане на нотификация към ECRIS-TCN
            public const string Active = "Active"; //	Актуален
            public const string ForDestruction = "ForDestruction"; //Подлежащ на унищожаване
            public const string Deleted = "Deleted"; //	Унищожен
            public const string ForRehabilitation = "ForRehabilitation"; //	Подлежащ на реабилитация на лицето
            public const string Rehabilitated = "Rehabilitated"; //	Извършена реабилитация
            public const string ForUpdate = "ForUpdate"; //	Актуализация на бюлетин
        }

        public static class Type
        {
            public const string ConvictionBulletin = "за съдимост";
            public const string Bulletin78А = "по чл.78а";
            public const string Unspecified = "неопределен";
        }
    }
}