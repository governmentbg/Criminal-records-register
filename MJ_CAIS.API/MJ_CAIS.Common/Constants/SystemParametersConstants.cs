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
            //колко месеца да е валидно свидетелството за съдимост
            public const string CERTIFICATE_VALIDITY_PERIOD_MONTHS = "CERTIFICATE_VALIDITY_PERIOD_MONTHS";
            //колко дни се очаква плащане за заявления подадени през уеб портала
            public const string TERM_FOR_PAYMENT_WEB_DAYS = "TERM_FOR_PAYMENT_WEB_DAYS";
            //колко дни се очаква плащане за заявления подадени на гише
            public const string TERM_FOR_PAYMENT_DESK_DAYS = "TERM_FOR_PAYMENT_DESK_DAYS";
        }
    }
}
