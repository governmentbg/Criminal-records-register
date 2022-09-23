import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonArchiveGridModel extends BaseGridModel {
  public type: string = null;
  public validFrom: Date = null;
  public csAuthority: string = null;
  public applicantName: string = null;
  public egn: string = null;
  public lnch: string = null;
  public fullName: string = null;
  public bithDate: Date = null;

  constructor(init?: Partial<PersonArchiveGridModel>) {
    super(init);
    this.type = init?.type ?? null;
    this.validFrom = init?.validFrom ?? null;  
    this.csAuthority = init?.csAuthority ?? null;
    this.applicantName = init?.applicantName ?? null;
    this.egn = init?.egn ?? null;
    this.lnch = init?.lnch ?? null;
    this.fullName = init?.fullName ?? null;
    this.bithDate = init?.bithDate ?? null;
  }
}
