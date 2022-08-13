import { BaseModel } from "../../../../@core/models/common/base.model";

export class PersonBulletinsGridModel extends BaseModel {
  public registrationNumber: string = null;
  public statusId: string = null;
  public statusName: string = null;
  public bulletinAuthorityName: string = null;
  public bulletinType: string = null;
  public remarks: string = null;
  public bulletinId: string = null;

  constructor(init?: Partial<PersonBulletinsGridModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.statusId = init?.statusId ?? null;
    this.statusName = init?.statusName ?? null;
    this.bulletinAuthorityName = init?.bulletinAuthorityName ?? null;
    this.bulletinType = init?.bulletinType ?? null;
    this.remarks = init?.remarks ?? null;
    this.bulletinId = init?.bulletinId ?? null;
  }
}
