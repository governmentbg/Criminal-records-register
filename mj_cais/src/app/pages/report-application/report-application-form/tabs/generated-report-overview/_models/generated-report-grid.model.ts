import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class GeneratedReportModel extends BaseModel {
  public registrationNumber: string = null;
  public firstSigner: string = null;
  public secondSigner: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;
  public docId: string = null;
  public statusCode: string = null;
  public statusName: string = null;

  constructor(init?: Partial<GeneratedReportModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstSigner = init?.firstSigner ?? null;
    this.secondSigner = init?.secondSigner ?? null;
    this.validFrom = init?.validFrom ?? null;
    this.validTo = init?.validTo ?? null;
    this.docId = init?.docId ?? null;
    this.statusCode = init?.statusCode ?? null;
    this.statusName = init?.statusName ?? null;
  }
}
