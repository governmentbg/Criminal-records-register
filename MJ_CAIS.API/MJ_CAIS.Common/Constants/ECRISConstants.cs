﻿using System;
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
            public const string Identified = "Identified";
            public const string ReplyCreated = "ReplyCreated";
        }
        public static class EcrisInboxStatuses
        {
            public const string Pending = "PENDING";
            public const string Processed = "PROCESSED";
            public const string Error = "ERROR";

        }
        public static class EcrisOutboxStatuses
        {
            public const string Pending = "PENDING";
            public const string Sent = "SENT";
            public const string Error = "ERROR";

        }
        public static class EcrisOutboxOperations
        {
            public const string StoreNotification = "STORE_NOTIFICATION";
            public const string StoreResponce = "STORE_RESPONSE";
        }

        public static class LanguageCodes
        {
            public const string Bg = "bg";
            public const string En = "en";
        }

        public static class EcrisTcnActionType
        {
            public const string Create = "CREATE";
            public const string Update = "UPDATE";
            public const string Delete = "DELETE";
        }
    }
}

