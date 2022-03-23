import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class EcrisMessageForm {
  public group: FormGroup;
  public id: FormControl;
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

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.decisionChTypeId = new FormControl(null);
    this.decisionChTypeName = new FormControl(null);
    this.decisionEcli = new FormControl(null);
    this.decisionNumber = new FormControl(null);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decisionAuthId = new FormControl(null);
    this.decisionAuthName = new FormControl(null);
    this.decisionTypeId = new FormControl(null);
    this.decisionTypeName = new FormControl(null);
    this.descr = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
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
    });
  }
}