export enum CertificateStatuTypeEnum {
  CertificateContentReady = "CertificateContentReady", //За попълване на подписващи - 1 или 2-ма
  BulletinsCheck = "BulletinsCheck", // Проверка на бюлетини от служител - БС по заявяване
  CertificatePaperPrint = "CertificatePaperPrint",
  Delivered= "Delivered",
  BulletinsSelection = "BulletinsSelection",
  BulletinsRehabilitation = "BulletinsRehabilitation",
  CertificateUserSign = "CertificateUserSign",
  CertificateForDelivery = "CertificateForDelivery",
  NewId = "NewId",//TODO: create app-enum
  ApprovedApplication = "ApprovedApplication",//TODO: create app-enum
}