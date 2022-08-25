import { BaseGridModel } from "../../../../../@core/models/common/base-grid.model";

export class BulletinAdministrationGridModel extends BaseGridModel {
  public registrationNumber: string;
  public bulletinAuthorityName: string;
  public bulletinType: string;
  public statusId: string;
  public statusName: string;
  public fullName: string;
  public identifier: Date;
  public birthDate: Date;

  constructor(init?: Partial<BulletinAdministrationGridModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.bulletinAuthorityName = init?.bulletinAuthorityName ?? null;
    this.bulletinType = init?.bulletinType ?? null;
    this.statusId = init?.statusId ?? null;
    this.statusName = init?.statusName ?? null;
    this.fullName = init?.fullName ?? null;
    this.identifier = init?.identifier ?? null;
    this.birthDate = init?.birthDate ?? null;
  }
}
