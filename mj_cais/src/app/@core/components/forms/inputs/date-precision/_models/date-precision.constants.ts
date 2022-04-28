import { BaseNomenclatureModel } from "../../../../../models/nomenclature/base-nomenclature.model";

export class DatePrecisionConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      DatePrecisionConstants.fullDate,
      DatePrecisionConstants.yearAndMonth,
      DatePrecisionConstants.year,
    ];
  }

  static get fullDate(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = DatePrecisionType.YMD;
    result.name = "Точна";
    return result;
  }

  static get yearAndMonth(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = DatePrecisionType.YM;
    result.name = "Година и месец";
    return result;
  }

  static get year() {
    let result = new BaseNomenclatureModel();
    result.id = DatePrecisionType.Y;
    result.name = "Година";
    return result;
  }
}

export enum DatePrecisionType {
  YMD = "YMD",
  YM = "YM",
  Y = "Y",
}
