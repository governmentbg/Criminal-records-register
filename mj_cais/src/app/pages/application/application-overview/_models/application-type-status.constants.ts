export enum ApplicationTypeStatusConstants {
  NewId = "NewId,FillApplication", // Ново заявление
  Canceled = "Canceled", // Анулирано
  CheckPayment = "CheckPayment", // Очаква потвърждение за плащане
  CheckTaxFree = "CheckTaxFree", // Освободени от плащане
  BulletinsCheck = "BulletinsCheck", // За обработка
  BulletinsSelection = "BulletinsSelection", // Очаква от съдия избор на бюлетини , съобразно  целта
  ForSigning = "CertificateContentReady,CertificatePaperPrint", // За подпис
  ForSigningByJudge = "CertificateContentReady,CertificateUserSign", // За подпис от съдя
  ApprovedApplication = "ApprovedApplication",
}
