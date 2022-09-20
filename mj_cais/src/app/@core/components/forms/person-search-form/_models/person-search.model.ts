import { BaseModel } from "../../../../models/common/base.model";

export class PersonSearchModel extends BaseModel {
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public fullname: string = null;
  public egn: string = null;
  public lnch: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<PersonSearchModel>) {
    super(init);
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.fullname = init?.fullname ?? null;
    this.egn = init?.egn ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.lnch = init?.lnch ?? null;
  }
}
