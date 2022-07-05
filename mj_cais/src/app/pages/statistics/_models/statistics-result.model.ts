import { BaseModel } from "../../../@core/models/common/base.model";

export class StatisticsResultModel extends BaseModel {
  public objectType: string = null;
  public count: number = null;

  constructor(init?: Partial<StatisticsResultModel>) {
    super(init);
    this.objectType = init?.objectType ?? null;
    this.count = init?.count ?? null;
  }
}
