import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../../../@core/models/common/base.form";

export class BulletinDecisionForm extends BaseForm {
  public group: FormGroup;
  public decisionChTypeId: FormControl;
  public decisionChTypeName: FormControl;
  public decisionEcli: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decisionAuthId: FormControl;
  public decisionAuthName: FormControl;
  public decisionTypeId: FormControl;
  public decisionTypeName: FormControl;
  public descr: FormControl;
  public changeDate: FormControl;

  constructor() {
    super();
    this.decisionChTypeId = new FormControl(null, [Validators.maxLength(50)]);
    this.decisionChTypeName = new FormControl(null);
    this.decisionEcli = new FormControl(null, [Validators.maxLength(100)]);
    this.decisionNumber = new FormControl(null, [Validators.maxLength(100)]);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decisionAuthId = new FormControl(null, [Validators.maxLength(50)]);
    this.decisionAuthName = new FormControl(null);
    this.decisionTypeId = new FormControl(null, [Validators.maxLength(50)]);
    this.decisionTypeName = new FormControl(null);
    this.descr = new FormControl(null);
    this.changeDate = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      decisionChTypeId: this.decisionChTypeId,
      decisionChTypeName: this.decisionChTypeName,
      decisionEcli: this.decisionEcli,
      decisionNumber: this.decisionNumber,
      decisionDate: this.decisionDate,
      decisionFinalDate: this.decisionFinalDate,
      decisionAuthId: this.decisionAuthId,
      decisionAuthName: this.decisionAuthName,
      decisionTypeId: this.decisionTypeId,
      decisionTypeName: this.decisionTypeName,
      descr: this.descr,
      changeDate: this.changeDate,
    });
  }
}
