import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class ReportAppStatusHistoryModel extends BaseModel {
  public status: string = null;
  public createdOn: string = null;
  public createdBy: string = null;
  public locked: string = null;
  public descr: string = null;

  constructor(init?: Partial<ReportAppStatusHistoryModel>) {
    super(init);
    this.status = init?.status ?? null;
    this.createdOn = init?.createdOn ?? null;
    this.createdBy = init?.createdBy ?? null;
    this.descr = init?.descr ?? null;
  }
}
