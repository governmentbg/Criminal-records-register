import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class ReportBulletinByPersonGridModel extends BaseGridModel {
  public registrationNumber: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public statusId: string = null;
  public statusName: string = null;
  public ln: string = null;
  public lnch: string = null;
  public egn: string = null;
  public bulletinType: string = null;

  constructor(init?: Partial<ReportBulletinByPersonGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.firstName = init.firstName ?? null;
      this.surName = init.surName ?? null;
      this.familyName = init.familyName ?? null;
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
