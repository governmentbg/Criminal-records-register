namespace MJ_CAIS.Common.Constants
{
    public class ApplicationConstants
    {
        public static class ApplicationStatuses
        {

            public const string NewId = "NewId";
            public const string FillApplication = "FillApplication";
            public const string Canceled = "Canceled";
            public const string CheckTaxFree = "CheckTaxFree";
            public const string CheckPayment = "CheckPayment";
            public const string ApprovedApplication = "ApprovedApplication";
            public const string BulletinsCheck = "BulletinsCheck";
            public const string BulletinsRehabilitation = "BulletinsRehabilitation";
            public const string BulletinsSelection = "BulletinsSelection";
            public const string CertificateContentReady = "CertificateContentReady";
            public const string CertificateServerSign = "CertificateServerSign";
            public const string CertificateUserSign = "CertificateUserSign";
            public const string CertificateForDelivery = "CertificateForDelivery";
            public const string CertificatePaperPrint = "CertificatePaperPrint";
            public const string Delivered = "Delivered";
            // TODO: remove web statuses:
            public const string NewWebApplication = "NewWebApplication";
            public const string WebDuplicateCheck = "WebDuplicateCheck";
            public const string WebRegistersChecks = "WebRegistersChecks";
            public const string WebCanceled = "WebCanceled";
            public const string WebCheckTaxFree = "WebCheckTaxFree";
            public const string WebCheckPayment = "WebCheckPayment";
            public const string WebApprovedApplication = "WebApprovedApplication";

            //todo: add to db
            public const string CertificateUserSigned = "CertificateUserSigned";
        }

        public static class ApplicationWebStatuses
        {
            public const string NewWebApplication = "NewWebApplication";
            public const string WebDuplicateCheck = "WebDuplicateCheck";
            public const string WebRegistersChecks = "WebRegistersChecks";
            public const string WebCanceled = "WebCanceled";
            public const string WebCheckTaxFree = "WebCheckTaxFree";
            public const string WebCheckPayment = "WebCheckPayment";
            public const string WebApprovedApplication = "WebApprovedApplication";
        }

        public static class ApplicationTypes
        {
            //todo: дали това са им имената
            // 4	Заявление за електронно свидетелство за съдимост
            //5	Заявление за електронно служебно свидетелство за съдимост
            //6	Заявление за Свидетелство за съдимост
            //7	Заявление за Справка за съдимост
            public const string WebCertificate = "4";
            public const string WebExternalCertificate = "5";
            public const string DeskCertificate = "6";
            public const string ConvictionRequest = "7";
            public const string ApplicationRequestOld = "8";
            public const string ConvictionRequestOld = "9";
        }
        public static class PaymentMethodsCodes
        {
            public const string Free = "FreeFromTax";
        }

        public static class ReceivingMethods
        {
            public const string EDelivery = "EDelivery";
        }

    }
}
