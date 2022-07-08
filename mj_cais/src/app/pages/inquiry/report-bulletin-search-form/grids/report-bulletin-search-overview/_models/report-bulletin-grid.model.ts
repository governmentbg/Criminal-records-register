import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class ReportBulletinGridModel extends BaseGridModel {
  public registrationNumber: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public statusId: string = null;
  public statusName: string = null;
  public ln: string = null;
  public lnch: string = null;
  public egn: string = null;
  public bulletinType: string = null;

  constructor(init?: Partial<ReportBulletinGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.statusId = init.statusId ?? null;
      this.statusName = init.statusName ?? null;
      this.createdOn = init.createdOn ?? null;
      this.ln = init.ln ?? null;
      this.lnch = init.lnch ?? null;
      this.egn = init.egn ?? null;
      this.bulletinType = init.bulletinType ?? null;
    }
  }
}
