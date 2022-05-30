export enum ApplicationTypeStatusConstants {
  NewId	= "NewId,FillApplication", // Ново заявление
  Cancelled = "Cancelled", // Анулирано
  CheckPayment = "CheckPayment", // Очаква потвърждение за плащане
  CheckTaxFree = "CheckTaxFree", // Освободени от плащане
  BulletinsCheck = "BulletinsCheck", // За обработка
  ForSigning = "CertificateContentReady,CertificatePaperPrint", // За подпис
  ForSigningByJudge = "CertificateContentReady,CertificatePaperPrint", // За подпис от съдя 
}
