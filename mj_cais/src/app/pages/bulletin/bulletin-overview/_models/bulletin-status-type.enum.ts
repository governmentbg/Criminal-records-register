export enum  BulletinStatusTypeEnum {
  NewEISS = "NewEISS", // Нов зареден от ЕИСС
  NewOffice = "NewOffice", //Нов въведен от служител на БС
  Active = "Active", //	Актуален
  ForDestruction = "ForDestruction", //Подлежащ на унищожаване
  Deleted = "Deleted", //	Унищожен
  ForRehabilitation = "ForRehabilitation", //	Подлежащ на реабилитация на лицето
  Rehabilitated = "Rehabilitated", //	Извършена реабилитация,
  ReplacedAct425 = "ReplacedAct425", // Постановен съдебен акт по чл. 425 НПК
  NoSanction = "NoSanction" // Деецът не е наказан
}