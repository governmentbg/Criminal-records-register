using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Common.Constants
{
    public class RegistrationConstants
    {
        public static class  RegisterCodes
        {
            //select ' //'||code||' - '||name
            //from D_REGISTER_TYPES t
            //order by code

            //001 - Заявление на гише
            //002 - Заявление за ЕС
            //003 - Заявление за ЕСС
            //004 - Искане за справка
            //005 - Заявление за Е-Справка
            //011 - Свидетелство на гише
            //012 - Свидетелство Електронно
            //013 - Електронно служебно свидетелство
            //014 - Справка за съдимост
            //015 - Електронна справка за съдимост
            //020 - Бюлетин за съдимост
            //021 - Бюлетин за 78а
            //022 - Бюлетин-неопределен
            //030 - Сведение - по пощата
            //031 - Сведение - от ЕКРИС
            //040 - ECRIS
            //050 - Заявка за БС

            public const string ApplicationOnDesk = "001";
            public const string ApplicationWeb = "002";
            public const string ApplicationWebExternal = "003";
            public const string CertificateOnDesk = "011";
            public const string CertificateWeb = "012";
            public const string CertificateWebExternal = "013";
            public const string Bulletin = "020";
            public const string Bulletin78a = "021";
            public const string BulletinUndefined = "022";
            public const string DecisionByPost = "030";
            public const string DecisionByEcris = "031";
            public const string Ecris = "040";
            public const string ConvictionRequest = "005"; //todo да се провери точно къде се ползва!!!
            public const string ConvictionResult = "015"; //todo -не се ползва 
            public const string ReportApplication = "004";//тодо - не се ползва
            public const string Report = "014"; //тодо - не се ползва
            public const string InternalRequest = "050";

        }
    }
}
