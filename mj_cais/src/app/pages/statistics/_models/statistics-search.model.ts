import { BaseModel } from "../../../@core/models/common/base.model";

export class StatisticsSearchModel extends BaseModel {
  public fromDate: Date = null;
  public toDate: Date = null;
  public authority: Date = null;

  constructor(init?: Partial<StatisticsSearchModel>) {
    super(init);
    this.fromDate = init?.fromDate ?? null;
    this.toDate = init?.toDate ?? null;
    this.authority = init?.authority ?? null;
  }
}
