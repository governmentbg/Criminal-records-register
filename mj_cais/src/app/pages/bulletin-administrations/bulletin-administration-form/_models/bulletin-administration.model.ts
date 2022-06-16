import { BaseModel } from "../../../../@core/models/common/base.model";

export class BulletinAdministrationModel extends BaseModel {
  public registrationNumber: string;
  public csAuthorityName: string;
  public statusName: string;
  public alphabeticalIndex: string;
  public ecrisConvictionId: string;
  public bulletinReceivedDate: Date;
  public bulletinTypeName: string;
  public bulletinAuthorityName: string;
  public bulletinCreateDate: Date;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public firstnameLat: string;
  public surnameLat: string;
  public familynameLat: string;
  public birthDate: Date;
  public egn: string;
  public lnch: string;
  public ln: string;

  constructor(init?: Partial<BulletinAdministrationModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.csAuthorityName = init?.csAuthorityName ?? null;
    this.statusName = init?.statusName ?? null;
    this.alphabeticalIndex = init?.alphabeticalIndex ?? null;
    this.ecrisConvictionId = init?.ecrisConvictionId ?? null;
    this.bulletinReceivedDate = init?.bulletinReceivedDate ?? null;
    this.bulletinTypeName = init?.bulletinTypeName ?? null;
    this.bulletinAuthorityName = init?.bulletinAuthorityName ?? null;
    this.bulletinCreateDate = init?.bulletinCreateDate ?? null;
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.firstnameLat = init?.firstnameLat ?? null;
    this.surnameLat = init?.surnameLat ?? null;
    this.familynameLat = init?.familynameLat ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.egn = init?.egn ?? null;
    this.lnch = init?.lnch ?? null;
    this.ln = init?.ln ?? null;
  }
}
