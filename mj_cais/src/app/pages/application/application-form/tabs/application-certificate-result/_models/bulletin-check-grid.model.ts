import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class BulletinCheckGridModel extends BaseGridModel {
  public registrationNumber: string = null;
  public bulletinReceivedDate: string = null;
  public statusId: string = null;
  public statusName: string = null;
  public bulletinAuthorityId: string = null;
  public bulletinAuthorityName: Date = null;

  constructor(init?: Partial<BulletinCheckGridModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.bulletinReceivedDate = init?.bulletinReceivedDate ?? null;
    this.statusId = init?.statusId ?? null;
    this.statusName = init?.statusName ?? null;
    this.bulletinAuthorityId = init?.bulletinAuthorityId ?? null;
    this.bulletinAuthorityName = init?.bulletinAuthorityName ?? null;
  }
}
