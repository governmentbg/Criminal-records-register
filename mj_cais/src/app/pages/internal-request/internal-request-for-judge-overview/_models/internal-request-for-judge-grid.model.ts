import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class InternalRequestForJudgeGridModel extends BaseGridModel {
  public regNumber: string = null;
  public statusName: string = null;
  public statusId: string = null;
  public requestDate: Date = null;
  public createdBy: string = null;
  public fromAuthorityName: string = null;
  public reqestType: string = null;

  constructor(init?: Partial<InternalRequestForJudgeGridModel>) {
    super(init);
    this.regNumber = init?.regNumber ?? null;
    this.statusName = init?.statusName ?? null;
    this.statusId = init?.statusId ?? null;
    this.requestDate = init?.requestDate ?? null;
    this.createdBy = init?.createdBy ?? null;
    this.fromAuthorityName = init?.fromAuthorityName ?? null;
    this.reqestType = init?.reqestType ?? null;
  }
}
