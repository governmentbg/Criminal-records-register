import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class ApplicationStatusHistoryModel extends BaseModel {
  public descr: string = null;
  public updatedBy: string = null;
  public createdOn: Date = null;
  public statusCode: string = null;


  constructor(init?: Partial<ApplicationStatusHistoryModel>) {
    super(init);
    this.descr = init?.descr ?? null;
    this.updatedBy = init?.updatedBy ?? null;
    this.createdOn = init?.createdOn ?? null;
    this.statusCode = init?.statusCode ?? null;
  }
}
