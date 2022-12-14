using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Common.Constants
{
    public class SystemParametersConstants
    {
        public static class SystemParametersNames
        {
            //урл адреса на публичния портал за граждани
            public const string WEB_PORTAL_URL = "WEB_PORTAL_URL";
            //име на сертификата, с който ще се подписва от сървъра
            public const string SYSTEM_SIGNING_CERTIFICATE_NAME = "SYSTEM_SIGNING_CERTIFICATE_NAME";
            //име на сертификата, с който ще се подписва от сървъра
            public const string SYSTEM_SIGNING_CERTIFICATE_NAME_FOR_DOWNLOAD = "DOWNLOAD_SIGNING_CERTIFICATE_NAME";
            //колко месеца да е валидно свидетелството за съдимост
            public const string CERTIFICATE_VALIDITY_PERIOD_MONTHS = "CERTIFICATE_VALIDITY_PERIOD_MONTHS";
            //колко дни се очаква плащане за заявления подадени през уеб портала
            public const string TERM_FOR_PAYMENT_WEB_DAYS = "TERM_FOR_PAYMENT_WEB_DAYS";
            //колко дни се очаква плащане за заявления подадени на гише
            public const string TERM_FOR_PAYMENT_DESK_DAYS = "TERM_FOR_PAYMENT_DESK_DAYS";           
            // 'Максимален брой опити до успешно извикване на Regix',
            public const string REGIX_NUMBER_OF_ATTEMPTS = "REGIX_NUMBER_OF_ATTEMPTS";
            //Брой дни в които regix кеша да се пази
            public const string REGIX_DAYS_CACHE = "REGIX_DAYS_CACHE";
            // 'Максимален брой опити до успешно изпращане през ЕДеливери',
            public const string EDELIVERY_NUMBER_OF_ATTEMPTS = "EDELIVERY_NUMBER_OF_ATTEMPTS";

            // 'IBAN на МП',
            public const string MJ_IBAN_BNB = "MJ_IBAN_BNB";
            //брой опити за екрис
            public const string ECRIS_MAX_NUMBER_OF_ATTEMPTS = "ECRIS_MAX_NUMBER_OF_ATTEMPTS";


        }
    }
}
