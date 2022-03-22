import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";

export class BulletinStatusTypeConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      BulletinStatusTypeConstants.newEISS, 
      BulletinStatusTypeConstants.newOffice, 
      BulletinStatusTypeConstants.expectPaper, 
      BulletinStatusTypeConstants.receivedPaper, 
      BulletinStatusTypeConstants.forEcris, 
      BulletinStatusTypeConstants.forEcrisTCN, 
      BulletinStatusTypeConstants.active, 
      BulletinStatusTypeConstants.forDestruction, 
      BulletinStatusTypeConstants.deleted, 
      BulletinStatusTypeConstants.forRehabilitation, 
      BulletinStatusTypeConstants.rehabilitated, 
      BulletinStatusTypeConstants.forUpdate,  
    ];
  }

  static get newEISS(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = "NewEISS";
    result.name = "Нов зареден от ЕИСС";
    return result;
  }

  static get newOffice() {
    let result = new BaseNomenclatureModel();
    result.id = "NewOffice";
    result.name = "Нов въведен от служител на БС";
    return result;
  }

  static get expectPaper() {
    let result = new BaseNomenclatureModel();
    result.id = "ExpectPaper";
    result.name = "Очакващ потвърждение за получаване на хартиено копие в БС по месторождение";
    return result;
  }

  static get receivedPaper() {
    let result = new BaseNomenclatureModel();
    result.id = "ReceivedPaper";
    result.name = "Потвърдено получаване в БС по месторождение";
    return result;
  }
 
  static get forEcris() {
    let result = new BaseNomenclatureModel();
    result.id = "ForEcris";
    result.name = "За изпращане на нотификация към ECRIS-RI";
    return result;
  }

  static get forEcrisTCN() {
    let result = new BaseNomenclatureModel();
    result.id = "ForEcrisTCN";
    result.name = "За изпращане на нотификация към ECRIS-TCN";
    return result;
  }

  static get active() {
    let result = new BaseNomenclatureModel();
    result.id = "Active";
    result.name = "Актуален";
    return result;
  }

  static get forDestruction() {
    let result = new BaseNomenclatureModel();
    result.id = "ForDestruction";
    result.name = "Подлежащ на унищожаване";
    return result;
  }

  static get deleted() {
    let result = new BaseNomenclatureModel();
    result.id = "ForDestruction";
    result.name = "Унищожен";
    return result;
  }

  static get forRehabilitation() {
    let result = new BaseNomenclatureModel();
    result.id = "ForRehabilitation";
    result.name = "Подлежащ на реабилитация на лицето";
    return result;
  }

  static get rehabilitated() {
    let result = new BaseNomenclatureModel();
    result.id = "Rehabilitated";
    result.name = "Извършена реабилитация";
    return result;
  }

  static get forUpdate() {
    let result = new BaseNomenclatureModel();
    result.id = "ForUpdate";
    result.name = "Актуализация на бюлетин";
    return result;
  }
}

export enum  BulletinStatusTypeEnum {
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