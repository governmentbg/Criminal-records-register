import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonBulletinGridModel extends BaseGridModel {
  public bulletinType: string = null;
  public statusName: string = null;
  public createdOn: Date = null;
  public registrationNumber: string = null;
  public alphabeticalIndex: string = null;
  public bulletinAuthorityName: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<PersonBulletinGridModel>) {
    super(init);
    this.bulletinType = init?.bulletinType ?? null;
    this.statusName = init?.statusName ?? null;
    this.createdOn = init?.createdOn ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.alphabeticalIndex = init?.alphabeticalIndex ?? null;
    this.bulletinAuthorityName = init?.bulletinAuthorityName ?? null;
    this.firstName = init?.firstName ?? null;
    this.surName = init?.surName ?? null;
    this.familyName = init?.familyName ?? null;
    this.birthDate = init?.birthDate ?? null;
  }
}
