import { BaseModel } from "../../../../@core/models/common/base.model";

export class ReportBulletinSearchModel extends BaseModel {
  public registrationNumber: string = null;
  public bulletinType: string = null;
  public bulletinReceivedDate: Date = null;
  public caseTypeId: string = null;
  public caseNumber: string = null;
  public caseYear: number = null;
  public decidingAuthId: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public decisionTypeId: string = null;
  public statusId: string = null;
  public offenceCatId: string = null;
  public sanctCategoryId: string = null;
  public fineAmount: number = null;

  constructor(init?: Partial<ReportBulletinSearchModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.bulletinType = init?.bulletinType ?? null;
    this.bulletinReceivedDate = init?.bulletinReceivedDate ?? null;
    this.caseTypeId = init?.caseTypeId ?? null;
    this.caseNumber = init?.caseNumber ?? null;
    this.caseYear = init?.caseYear ?? null;
    this.decidingAuthId = init?.decidingAuthId ?? null;
    this.decisionNumber = init?.decisionNumber ?? null;
    this.decisionDate = init?.decisionDate ?? null;
    this.decisionFinalDate = init?.decisionFinalDate ?? null;
    this.decisionTypeId = init?.decisionTypeId ?? null;
    this.statusId = init?.statusId ?? null;
    this.offenceCatId = init?.offenceCatId ?? null;
    this.sanctCategoryId = init?.sanctCategoryId ?? null;
    this.fineAmount = init?.fineAmount ?? null;
  }
}
