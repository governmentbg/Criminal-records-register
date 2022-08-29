import { FormControl, FormGroup, Validators } from "@angular/forms";
import { LookupForm } from "../../../../@core/components/forms/inputs/lookup/models/lookup.form";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class InternalRequestForm extends BaseForm {
  public group: FormGroup;

  public regNumber: FormControl;
  public regNumberDisplay: FormControl;
  public description: FormControl;
  public reqStatusCode: FormControl;
  public responseDescr: FormControl;
  public requestDate: FormControl;
  public pPersIdId: FormControl;
  public pPersIdIdDisplay: FormControl;
  public pPidTypeCode: FormControl;
  public reqStatusName: FormControl;
  public fromAuthorityId: FormControl;
  public toAuthorityId: FormControl;
  public nIntReqTypeId: FormControl;
  public selectedBulletinsTransactions: FormControl;

  constructor() {
    super();
    this.regNumber = new FormControl(null);
    this.regNumberDisplay = new FormControl(null);
    this.regNumberDisplay.disable();
    this.description = new FormControl(null, Validators.required);
    this.reqStatusCode = new FormControl(null);
    this.responseDescr = new FormControl(null);
    this.requestDate = new FormControl(new Date());
    this.pPersIdId = new FormControl(null, Validators.required);
    this.pPersIdIdDisplay = new FormControl(null);
    this.pPidTypeCode = new FormControl(null);
    this.pPersIdIdDisplay.disable();
    this.reqStatusName = new FormControl(null);
    this.fromAuthorityId = new FormControl(null);
    this.toAuthorityId = new FormControl(null, Validators.required);
    this.nIntReqTypeId = new FormControl(null, Validators.required);
    this.selectedBulletinsTransactions = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      regNumber: this.regNumber,
      regNumberDisplay: this.regNumberDisplay,
      description: this.description,
      reqStatusCode: this.reqStatusCode,
      responseDescr: this.responseDescr,
      requestDate: this.requestDate,
      pPersIdId: this.pPersIdId,
      pPidTypeCode: this.pPidTypeCode,
      pPersIdIdDisplay: this.pPersIdIdDisplay,
      reqStatusName: this.reqStatusName,
      fromAuthorityId: this.fromAuthorityId,
      toAuthorityId: this.toAuthorityId,
      nIntReqTypeId: this.nIntReqTypeId,
      selectedBulletinsTransactions: this.selectedBulletinsTransactions,
    });
  }
}
