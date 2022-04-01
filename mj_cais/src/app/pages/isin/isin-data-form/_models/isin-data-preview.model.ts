import { BulletinPersonInfoModel } from "../../../../@core/components/shared/bulletin-person-info/_models/bulletin-person-info.model";
import { IsinDataModel } from "./isin-data.model";

export class IsinDataPreviewModel extends IsinDataModel {
  public bulletinPersonInfo: BulletinPersonInfoModel = null;

  constructor(init?: Partial<IsinDataPreviewModel>) {
    super(init);
    this.bulletinPersonInfo = init?.bulletinPersonInfo ?? null;
  }
}