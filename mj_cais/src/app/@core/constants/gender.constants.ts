import { BaseNomenclatureModel } from "../models/nomenclature/base-nomenclature.model";

export class GenderConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [GenderConstants.male, GenderConstants.female ,GenderConstants.unknown];
  }

  static get unknown(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 0;
    result.name = "Неизвестен";
    return result;
  }

  static get male(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 1;
    result.name = "Мъж";
    return result;
  }

  static get female() {
    let result = new BaseNomenclatureModel();
    result.id = 2;
    result.name = "Жена";
    return result;
  }
}
