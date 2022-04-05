import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";

export class IsinDataStatusConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      IsinDataStatusConstants.new,
      IsinDataStatusConstants.identified,
      IsinDataStatusConstants.unidentified,
      IsinDataStatusConstants.closed,
    ];
  }

  static get new(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 1;
    result.code = IsinDataStatusCodeConstants.New;
    result.name = IsinDataStatusNameConstants.New;
    return result;
  }

  static get identified(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 1;
    result.code = IsinDataStatusCodeConstants.Identified;
    result.name = IsinDataStatusNameConstants.Identified;
    return result;
  }

  static get unidentified(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 1;
    result.code = IsinDataStatusCodeConstants.Unidentified;
    result.name = IsinDataStatusNameConstants.Unidentified;
    return result;
  }

  static get closed(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 1;
    result.code = IsinDataStatusCodeConstants.Closed;
    result.name = IsinDataStatusNameConstants.Closed;
    return result;
  }
}

export enum IsinDataStatusCodeConstants {
  New = "New",
  Identified = "Identified",
  Unidentified = "Unidentified",
  Closed = "Closed",
}

export enum IsinDataStatusNameConstants {
  New = "Новопостъпил и неразпознат",
  Identified = "Разпознат",
  Unidentified = "Невъзможен за разпознаване",
  Closed = "Приключила обработка",
}