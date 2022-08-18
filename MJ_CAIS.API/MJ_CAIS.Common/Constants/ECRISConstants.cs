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
            public const string Identified = "Identified";
            public const string ReplyCreated = "ReplyCreated";
            public const string Error = "Error";
        }
        public static class EcrisInboxStatuses
        {
            public const string Pending = "PENDING_IN";
            public const string Processed = "PROCESSED_IN";
            public const string Error = "ERROR_IN";

        }
        public static class EcrisOutboxStatuses
        {
            public const string Pending = "PENDING_OUT";
            public const string Sent = "SENT_OUT";
            public const string Error = "ERROR_OUT";

        }
        public static class EcrisOutboxOperations
        {
            public const string StoreNotification = "STORE_NOTIFICATION";
            public const string StoreResponce = "STORE_RESPONSE";
            public const string StoreNotResponce = "STORE_NOT_RESPONSE";
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

        public static class EcrisTcnStatus
        {
            public const string New = "Нов";
            public const string Approved = "Обработен";
            public const string Canceled = "Отказан";
        }


        public static class EcrisRequestTypes
        {
            public const string REQUEST_SUCCESSFUL = "RRT-00-00";
            public const string REQUEST_DENIAL = "RRT-00-01";
            public const string REQUEST_NOT_FROM_MEMBER_STATE = "RRT-00-02";
            public const string REQUEST_DEAD_PERSON = "RRT-00-03";
            public const string REQUEST_NIST_NOT_MATCH = "RRT-00-04";
            public const string REQUEST_MULTIPLE_PEOPLE_FOUND = "RRT-00-05";
        }

        public static class EcrisNomenclatureCode
        {
            public const string NotificationResponses = "NotificationResponses";
            public const string RequestResponses = "RequestResponses";
        }
    }
}

