export enum EcrisMessageStatusConstants {
  ForIdentification = "ForIdentification", // Чака идентификация
  Unidentified = "Unidentified", // Неидентифицирано лице
  Identified = "Identified", // Идентифицирано лице
  NotForFBBC = "NotForFBBC", // Сведение за осъдено в чужбина лице
  ReqWithRespUnconvicted = "ReqWithRespUnconvicted", // Изпратен отговор за неосъждано лице
  ReqWaitingForCSAuthority = "ReqWaitingForCSAuthority", // Запитване, чакащо отговор за решение към БС по месторождение
  ReqWihtRespConvicted = "ReqWihtRespConvicted", // Изготвен отговор на запитване от БС по месторождение
}
