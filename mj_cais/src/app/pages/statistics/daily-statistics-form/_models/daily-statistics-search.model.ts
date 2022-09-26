import { BaseModel } from "../../../../@core/models/common/base.model";

export class DailyStatisticsSearchModel extends BaseModel {
  public fromDate: Date = null;
  public toDate: Date = null;
  public statisticsType: string = null;
  public status: string = null;

  constructor(init?: Partial<DailyStatisticsSearchModel>) {
    super(init);
    this.fromDate = init?.fromDate ?? null;
    this.toDate = init?.toDate ?? null;
    this.statisticsType = init?.statisticsType ?? null;
    this.status = init?.status ?? null;
  }
}
