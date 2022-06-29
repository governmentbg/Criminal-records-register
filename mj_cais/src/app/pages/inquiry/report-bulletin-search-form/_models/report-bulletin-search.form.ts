import { FormControl, FormGroup } from "@angular/forms";
export class ReportBulletinSearchForm  {
  public group: FormGroup;

  public registrationNumber: FormControl;
  public bulletinType: FormControl;
  public bulletinReceivedDate: FormControl;
  public caseTypeId: FormControl;
  public caseNumber: FormControl;
  public caseYear: FormControl;
  public decidingAuthId: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decisionTypeId: FormControl;
  public statusId: FormControl;
  public offenceCatId: FormControl;
  public sanctCategoryId: FormControl;
  public fineAmount: FormControl;

  constructor() {
    this.registrationNumber = new FormControl(null);
    this.bulletinType = new FormControl(null);
    this.bulletinReceivedDate = new FormControl(null);
    this.caseTypeId = new FormControl(null);
    this.caseNumber = new FormControl(null);
    this.caseYear = new FormControl(null);
    this.decidingAuthId = new FormControl(null);
    this.decisionNumber = new FormControl(null);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decisionTypeId = new FormControl(null);
    this.statusId = new FormControl(null);
    this.offenceCatId = new FormControl(null);
    this.sanctCategoryId = new FormControl(null);
    this.fineAmount = new FormControl(null);

    this.group = new FormGroup({
      registrationNumber: this.registrationNumber,
      bulletinType: this.bulletinType,
      bulletinReceivedDate: this.bulletinReceivedDate,
      caseTypeId: this.caseTypeId,
      caseNumber: this.caseNumber,
      caseYear: this.caseYear,
      decidingAuthId: this.decidingAuthId,
      decisionNumber: this.decisionNumber,
      decisionDate: this.decisionDate,
      decisionFinalDate: this.decisionFinalDate,
      decisionTypeId: this.decisionTypeId,
      statusId: this.statusId,
      offenceCatId: this.offenceCatId,
      sanctCategoryId: this.sanctCategoryId,
      fineAmount: this.fineAmount,
    });
  }
}
