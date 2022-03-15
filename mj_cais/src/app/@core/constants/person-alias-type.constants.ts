import { BaseNomenclatureModel } from "../models/nomenclature/base-nomenclature.model";

export class PersonAliasConstants {
  static get allData(): BaseNomenclatureModel[] {
    //nickname - псевдоним,previous - предишно име, maiden - моминско име
    return [
      PersonAliasConstants.nickname,
      PersonAliasConstants.previous,
      PersonAliasConstants.maiden,
    ];
  }

  static get nickname(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 1;
    result.code = PersonAliasCodeConstants.Nickname;
    result.name = PersonAliasNameConstants.Nickname;
    return result;
  }

  static get previous(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 2;
    result.code = PersonAliasCodeConstants.Previous;
    result.name = PersonAliasNameConstants.Previous;
    return result;
  }

  static get maiden(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.id = 3;
    result.code = PersonAliasCodeConstants.Maiden;
    result.name = PersonAliasNameConstants.Maiden;
    return result;
  }
}

export enum PersonAliasCodeConstants {
  Nickname = "nickname",
  Previous = "previous",
  Maiden = "maiden",
}

export enum PersonAliasNameConstants {
  Nickname = "Псевдоним",
  Previous = "Предишно име",
  Maiden = "Моминско име",
}
