import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class GeneratedReportModel extends BaseModel {
  public registrationNumber: string = null;
  public firstSigner: string = null;
  public firstSignerId: string = null;
  public secondSigner: string = null;
  public secondSignerId: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;
  public docId: string = null;
  public statusCode: string = null;
  public statusName: string = null;
  public bulletinsCount: number = null;

  constructor(init?: Partial<GeneratedReportModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstSigner = init?.firstSigner ?? null;
    this.firstSignerId = init?.firstSignerId ?? null;
    this.secondSigner = init?.secondSigner ?? null;
    this.secondSignerId = init?.secondSignerId ?? null;
    this.validFrom = init?.validFrom ?? null;
    this.validTo = init?.validTo ?? null;
    this.docId = init?.docId ?? null;
    this.statusCode = init?.statusCode ?? null;
    this.statusName = init?.statusName ?? null;
    this.bulletinsCount = init?.bulletinsCount ?? null;
  }
}
