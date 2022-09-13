import { BaseGridModel } from "../../../../../@core/models/common/base-grid.model";

export class InternalRequestMailBoxGridModel extends BaseGridModel {
  public regNumber: string = null;
  public reqStatusCode: string = null;
  public reqStatusName: string = null;
  public requestDate: Date = null;
  public createdBy: string = null;
  public reqestType: Date = null;
  public fromAuthorityName: string = null;
  public toAuthorityName: string = null;
  public pid: string = null;

  constructor(init?: Partial<InternalRequestMailBoxGridModel>) {
    super(init);
    this.regNumber = init?.regNumber ?? null;
    this.reqStatusCode = init?.reqStatusCode ?? null;
    this.reqStatusName = init?.reqStatusName ?? null;
    this.requestDate = init?.requestDate ?? null;
    this.createdBy = init?.createdBy ?? null;
    this.reqestType = init?.reqestType ?? null;
    this.fromAuthorityName = init?.fromAuthorityName ?? null;
    this.toAuthorityName = init?.toAuthorityName ?? null;
    this.pid = init?.pid ?? null;
  }
}
