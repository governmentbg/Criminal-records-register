import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonGeneratedReportGridModel extends BaseGridModel {
  public registrationNumber: string;
  public purpose: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public statusName: string;
  public birthDate: Date;
  public createdOn: Date;
  
  constructor(init?: Partial<PersonGeneratedReportGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.purpose = init.purpose ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.birthDate = init.birthDate ?? null;
      this.statusName = init.statusName ?? null;
      this.createdOn = init.createdOn ?? null;
    }
  }
}
