import { BaseModel } from "../../../../../../../../@core/models/common/base.model";

export class ApplicationCertificateResultBulletionPreviewModel extends BaseModel {
  public decisionTypeId: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public decidingAuthId: string = null;
  public decisionEcli: string = null;

  public caseTypeId: string = null;
  public caseNumber: string = null;
  public caseYear: number = null;
  public caseAuthId: string = null;
  public convRemarks: string = null;

  constructor(
    init?: Partial<ApplicationCertificateResultBulletionPreviewModel>
  ) {
    super(init);

    this.decisionTypeId = init?.decisionTypeId ?? null;
    this.decisionNumber = init?.decisionNumber ?? null;
    this.decisionDate = init?.decisionDate ?? null;
    this.decisionFinalDate = init?.decisionFinalDate ?? null;
    this.decidingAuthId = init?.decidingAuthId ?? null;
    this.decisionEcli = init?.decisionEcli ?? null;
    this.caseTypeId = init?.caseTypeId ?? null;
    this.caseNumber = init?.caseNumber ?? null;
    this.caseYear = init?.caseYear ?? null;
    this.caseAuthId = init?.caseAuthId ?? null;
    this.convRemarks = init?.convRemarks ?? null;
  }
}
