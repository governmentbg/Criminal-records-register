import { FormControl, FormGroup } from "@angular/forms";
import { BaseForm } from "../../../../../../../../@core/models/common/base.form";

export class ApplicationCertificateResultBulletionPreviewForm extends BaseForm {
  public group: FormGroup;

  public decisionTypeId: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decidingAuthId: FormControl;
  public decisionEcli: FormControl;

  public caseTypeId: FormControl;
  public caseNumber: FormControl;
  public caseYear: FormControl;
  public caseAuthId: FormControl;
  public convRemarks: FormControl;

  constructor( isEdit: boolean, locked: boolean) {
    super();
    this.initFormControls(locked);

    this.initGroup();
  }

  private initGroup(): void {
    this.group = new FormGroup({
      id: this.id,
      version: this.version,

      decisionNumber: this.decisionNumber,
      decisionDate: this.decisionDate,
      decisionFinalDate: this.decisionFinalDate,
      decidingAuthId: this.decidingAuthId,
      decisionTypeId: this.decisionTypeId,
      decisionEcli: this.decisionEcli,

      caseTypeId: this.caseTypeId,
      caseNumber: this.caseNumber,
      caseYear: this.caseYear,
      caseAuthId: this.caseAuthId,
      convRemarks: this.convRemarks,
    });
  }

  private initFormControls(locked: boolean): void {
    this.id = new FormControl(null);

    this.decisionTypeId = new FormControl(null);
    this.decisionNumber = new FormControl(null);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decidingAuthId = new FormControl(null);
    this.decisionEcli = new FormControl(null);

    this.caseTypeId = new FormControl(null);
    this.caseNumber = new FormControl(null);
    this.caseYear = new FormControl(null);
    this.caseAuthId = new FormControl(null);
    this.convRemarks = new FormControl(null);
  }
}
