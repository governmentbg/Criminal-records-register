import { BaseModel } from "../../../../../@core/models/common/base.model";

export class ApplicationGridModel extends BaseModel {
  public registrationNumber: string;
  public purpose: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public StatusCode: string;
  public birthDate: Date;
  public birthPlaceOther: string;
  public csAuthorityBirth: string;

  constructor(init?: Partial<ApplicationGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.purpose = init.purpose ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthPlaceOther = init.birthPlaceOther ?? null;
      this.StatusCode = init.StatusCode ?? null;
      this.csAuthorityBirth = init.csAuthorityBirth ?? null;
    }
  }
}
