import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";

export class DailyStatisticsConstants {
  static get allData(): BaseNomenclatureModel[] {
    return [
      DailyStatisticsConstants.Bulletin,
      DailyStatisticsConstants.Certificate,
      DailyStatisticsConstants.Report,
      DailyStatisticsConstants.Application,
      DailyStatisticsConstants.ReportApplication,
    ];
  }

  static get Bulletin(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "Bulletin";
    result.id = result.code;
    result.name = "Бюлетин";
    return result;
  }

  static get Certificate(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "Certificate";
    result.id = result.code;
    result.name = "Свидетелство";
    return result;
  }
  static get Report(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "Report";
    result.id = result.code;
    result.name = "Справка";
    return result;
  }
  static get Application(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "Application";
    result.id = result.code;
    result.name = "Заявление за свидетелство";
    return result;
  }
  static get ReportApplication(): BaseNomenclatureModel {
    let result = new BaseNomenclatureModel();
    result.code = "ReportApplication";
    result.id = result.code;
    result.name = "Искане за справка";
    return result;
  }
}
