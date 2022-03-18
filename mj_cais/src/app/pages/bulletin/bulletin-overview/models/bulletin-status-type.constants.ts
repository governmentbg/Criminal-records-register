export enum BulletinStatusTypeConstants {
  NewEISS = "NewEISS", // Нов зареден от ЕИСС
  NewOffice = "NewOffice", //Нов въведен от служител на БС
  ExpectPaper = "ExpectPaper", // Очакващ потвърждение за получаване на хартиено копие в БС по месторождение
  ReceivedPaper = "ReceivedPaper", // Потвърдено получаване в БС по месторождение
  ForEcris = "ForEcris", //За изпращане на нотификация към ECRIS-RI
  ForEcrisTCN = "ForEcrisTCN", //За изпращане на нотификация към ECRIS-TCN
  Active = "Active", //	Актуален
  ForDestruction = "ForDestruction", //Подлежащ на унищожаване
  Deleted = "Deleted", //	Унищожен
  ForRehabilitation = "ForRehabilitation", //	Подлежащ на реабилитация на лицето
  Rehabilitated = "Rehabilitated", //	Извършена реабилитация
  ForUpdate = "ForUpdate", //	Актуализация на бюлетин
}