import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class ApplicationCertificateResultModel extends BaseModel {
  public applicationId: string = null;
  public statusCode: string = null;
  public registrationNumber: string = null;
  public firstSignerId: string = null;
  public secondSignerId: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;

  constructor(init?: Partial<ApplicationCertificateResultModel>) {
    super(init);
    this.applicationId = init?.applicationId ?? null;
    this.statusCode = init?.statusCode ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstSignerId = init?.firstSignerId ?? null;
    this.secondSignerId = init?.secondSignerId ?? null;
    this.validFrom = init?.validFrom ?? null;
    this.validTo = init?.validTo ?? null;
  }
}
