using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Common.Constants
{
    public class ECRISConstants
    {
        public static class EcrisMessageStatuses
        {
            public const string ForIdentification = "ForIdentification";

            public const string Unidentified = "Unidentified";
            public const string NotForFBBC = "NotForFBBC";
            public const string ReqWithRespUnconvicted = "ReqWithRespUnconvicted";
            public const string ReqWaitingForCSAuthority = "ReqWaitingForCSAuthority";
            public const string ReqWihtRespConvicted = "ReqWihtRespConvicted";
            public const string NotificationCreated = "NotificationCreated";
            public const string ResponceCreated = "ResponceCreated";
            public const string ForSending = "ForSending";
            public const string Sent = "Sent";
        }
        public static class EcrisInboxStatuses
        {
            public const string Pending = "PENDING";
            public const string Processed = "PROCESSED";
            public const string Error = "ERROR";

        }

        public static class LanguageCodes
        {
            public const string Bg = "bg";
            public const string En = "en";
        }

    }
}

