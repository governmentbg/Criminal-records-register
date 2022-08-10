import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class AllGeneratedReportGridModel extends BaseGridModel {
  public registrationNumber: string;
  public reportApplRegNumber: string;
  public reportApplId: string;
  public purpose: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public statusName: string;
  public birthDate: Date;
  public createdOn: Date;
  
  constructor(init?: Partial<AllGeneratedReportGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.reportApplRegNumber = init.reportApplRegNumber ?? null;
      this.reportApplId = init.reportApplId ?? null;
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
