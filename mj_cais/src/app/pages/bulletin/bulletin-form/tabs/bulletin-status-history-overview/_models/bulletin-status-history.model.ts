import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class BulletinStatusHistoryModel extends BaseModel {
  public oldStatus: string = null;
  public newStatus: string = null;
  public createdOn: string = null;
  public createdBy: string = null;
  public locked: string = null;
  public descr: string = null;
  public hasContent: boolean = null;

  constructor(init?: Partial<BulletinStatusHistoryModel>) {
    super(init);
    this.oldStatus = init?.oldStatus ?? null;
    this.newStatus = init?.newStatus ?? null;
    this.createdOn = init?.createdOn ?? null;
    this.createdBy = init?.createdBy ?? null;
    this.locked = init?.locked ?? null;
    this.descr = init?.descr ?? null;
    this.hasContent = init?.hasContent ?? null;
  }
}
