import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class ApplicationCertificateResultModel extends BaseModel  {
  public firstSignerId: string = null;
  public secondSignerId: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;

  constructor(init?: Partial<ApplicationCertificateResultModel>) {
    super(init);
      this.firstSignerId = init?.firstSignerId ?? null;
      this.secondSignerId = init?.secondSignerId ?? null;
      this.validFrom = init?.validFrom ?? null;
      this.validTo = init?.validTo ?? null;   
  }
}
