import { BaseGridModel } from "../../../../../@core/models/common/base-grid.model";

export class ApplicationGridModel extends BaseGridModel {
  public registrationNumber: string;
  public purpose: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public statusCode: string;
  public statusName: string;
  public birthDate: Date;
  public birthPlaceOther: string;
  public csAuthorityBirth: string;
  public createdOn: Date;
  
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
      this.statusCode = init.statusCode ?? null;
      this.statusName = init.statusName ?? null;
      this.csAuthorityBirth = init.csAuthorityBirth ?? null;
      this.createdOn = init.createdOn ?? null;
    }
  }
}
