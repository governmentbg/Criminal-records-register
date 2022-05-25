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

            //            101	Заявление на гише
            public const string ApplicationOnDesk = "101";
            //102	Заявление за ЕС
            public const string ApplicationWeb = "102";
            //103	Заявление за ЕСС
            public const string ApplicationWebInternal = "103";
            //201	Свидетелство на гише
            public const string CertificateOnDesk = "201";
            //202	Свидетелство Електронно
            public const string CertificateWeb = "202";
            //203	Електронно служебно свидетелство
            public const string CertificateWebInternal = "203";
            //301	Бюлетин за съдимост
            public const string Bulletin = "301";
            //302	Бюлетин за 78а
            public const string Bulletin78a = "302";
            //303	Бюлетин-неопределен
            public const string BulletinUndefined = "303";
            //402	Сведение - по пощата
            public const string DecisionByPost = "401";
            //405	Сведение - от ЕКРИС
            public const string DecisionByEcris = "402";
            //501	ECRIS
            public const string Ecris = "501";
            //601	Справка за съдимост
            public const string ConvictionRequest = "601";


        }
    }
}
