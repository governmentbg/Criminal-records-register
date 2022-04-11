import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";

export class BulletinTypeConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      BulletinTypeConstants.convictionBulletin,
      BulletinTypeConstants.bulletin78А,
      BulletinTypeConstants.unspecified,
    ];
  }

  static get convictionBulletin(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "ConvictionBulletin";
    result.id = result.code;
    result.name = "за съдимост";
    return result;
  }

  static get bulletin78А(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "Bulletin78А";
    result.id = result.code;
    result.name = "по чл.78а";
    return result;
  }

  static get unspecified(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "Unspecified";
    result.id = result.code;
    result.name = "неопределен";
    return result;
  }
}