﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Common.Constants
{
    public class ApplicationConstants
    {
        public static class ApplicationStatuses
        {
     
            public const string NewId= "NewId";
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
        public static class ApplicationTypes
        {
            public const string InternalCode5 = "5";
        }
        public static class PaymentMethodsCodes
        {
            public const string Free = "Free";
        }

        public static class ReceivingMethods
        {
            public const string EDelivery = "EDelivery";
        }

    }
}