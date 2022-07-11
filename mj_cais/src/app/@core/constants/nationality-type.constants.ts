import { BaseNomenclatureModel } from "../models/nomenclature/base-nomenclature.model";

export class NationalityTypeConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      NationalityTypeConstants.currentCountry,
      NationalityTypeConstants.eu,
      NationalityTypeConstants.tcn,
      NationalityTypeConstants.bgAndEu,
      NationalityTypeConstants.bgAndTcn,
      NationalityTypeConstants.euAndTcn,
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
    result.name = "Гражданин на трета страна";
    return result;
  }

  static get bgAndEu() {
    let result = new BaseNomenclatureModel();
    result.code = "BG_EU";
    result.id = result.code;
    result.name = "Гражданин на България и ЕС";
    return result;
  }

  static get bgAndTcn() {
    let result = new BaseNomenclatureModel();
    result.code = "BG_TCN";
    result.id = result.code;
    result.name = "Гражданин на трета страна и България";
    return result;
  }

  static get euAndTcn() {
    let result = new BaseNomenclatureModel();
    result.code = "EU_TCN";
    result.id = result.code;
    result.name = "Гражданин на трета страна и ЕС";
    return result;
  }
}
