import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonBulletinGridModel extends BaseGridModel {
  public bulletinType: string = null;
  public statusName: string = null;
  public createdOn: Date = null;
  public registrationNumber: string = null;
  public bulletinAuthorityName: string = null;
  public caseNumberAndYear: string = null;
  public birthDate: Date = null;
  public egn: string = null;
  public lnch: string = null;
  public fullName: string = null;
  public csAuthorityId: string = null;

  constructor(init?: Partial<PersonBulletinGridModel>) {
    super(init);
    this.bulletinType = init?.bulletinType ?? null;
    this.statusName = init?.statusName ?? null;
    this.createdOn = init?.createdOn ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.caseNumberAndYear = init?.caseNumberAndYear ?? null;
    this.bulletinAuthorityName = init?.bulletinAuthorityName ?? null;
    this.egn = init?.egn ?? null;
    this.lnch = init?.lnch ?? null;
    this.fullName = init?.fullName ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.csAuthorityId = init?.csAuthorityId ?? null;
  }
}
