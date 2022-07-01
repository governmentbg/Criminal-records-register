import { BaseNomenclatureModel } from "../models/nomenclature/base-nomenclature.model";

export class NationalityTypeConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      NationalityTypeConstants.currentCountry,
      NationalityTypeConstants.eu,
      NationalityTypeConstants.tcn,
      NationalityTypeConstants.bgAndEu,
      NationalityTypeConstants.bgAndTcn,
    ];
  }

  static get currentCountry(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "COUNTRY";
    result.id = result.code;
    result.name = "Конкретна държава";
    return result;
  }

  static get eu(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "EU";
    result.id = result.code;
    result.name = "ЕС";
    return result;
  }

  static get tcn() {
    let result = new BaseNomenclatureModel();
    result.code = "TCN";
    result.id = result.code;
    result.name = "TCN";
    return result;
  }

  static get bgAndEu() {
    let result = new BaseNomenclatureModel();
    result.code = "BG_EU";
    result.id = result.code;
    result.name = "БГ и ЕС";
    return result;
  }

  static get bgAndTcn() {
    let result = new BaseNomenclatureModel();
    result.code = "BG_TCN";
    result.id = result.code;
    result.name = "БГ и TCN";
    return result;
  }
}
