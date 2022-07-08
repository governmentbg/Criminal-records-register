import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonApplicationGridModel extends BaseGridModel {
  public type: string = null;
  public certificateStatus: string = null;
  public certifivateRegistrationNumber: string = null;
  public certifivateValidDate: Date = null;
  public csAuthority: string = null;
  public applicantName: string = null;
  public egn: string = null;
  public lnch: string = null;
  public fullName: string = null;
  public bithDate: Date = null;

  constructor(init?: Partial<PersonApplicationGridModel>) {
    super(init);
    this.type = init?.type ?? null;
    this.certificateStatus = init?.certificateStatus ?? null;
    this.certifivateRegistrationNumber =
      init?.certifivateRegistrationNumber ?? null;
    this.certifivateValidDate = init?.certifivateValidDate ?? null;
    this.csAuthority = init?.csAuthority ?? null;
    this.applicantName = init?.applicantName ?? null;
    this.egn = init?.egn ?? null;
    this.lnch = init?.lnch ?? null;
    this.fullName = init?.fullName ?? null;
    this.bithDate = init?.bithDate ?? null;
  }
}
