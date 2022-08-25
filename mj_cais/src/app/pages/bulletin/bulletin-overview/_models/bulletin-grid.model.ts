import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class BulletinGridModel extends BaseGridModel {
  public registrationNumber: string = null;
  public fullName: string = null;
  public statusId: string = null;
  public statusName: string = null;
  public createdOn: Date = null;
  public alphabeticalIndex: string = null;
  public bulletinAuthorityName: string = null;
  public identifier: string = null;
  public deleteDate: Date = null;
  public rehabilitationDate: Date = null;
  public bulletinType: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<BulletinGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.fullName = init.fullName ?? null;
      this.statusId = init.statusId ?? null;
      this.statusName = init.statusName ?? null;
      this.createdOn = init.createdOn ?? null;
      this.alphabeticalIndex = init.alphabeticalIndex ?? null;
      this.bulletinAuthorityName = init.bulletinAuthorityName ?? null;
      this.identifier = init.identifier ?? null;
      this.deleteDate = init.deleteDate ?? null;
      this.rehabilitationDate = init.rehabilitationDate ?? null;
      this.bulletinType = init.bulletinType ?? null;
      this.birthDate = init.birthDate ?? null;
    }
  }
}
