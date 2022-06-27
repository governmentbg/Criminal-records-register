import { FormControl, FormGroup } from "@angular/forms";
import { BaseForm } from "../../../../../../@core/models/common/base.form";

export class ApplicationCertificateResultForm extends BaseForm {
  public group: FormGroup;

  public firstSignerId: FormControl;
  public secondSignerId: FormControl;
  public validFrom: FormControl;
  public validTo: FormControl;

  public applicationId: FormControl;
  public statusCode: FormControl;
  public docName: FormControl;
  public statusName: FormControl;
  public registrationNumber: FormControl;
  public currentUserAuthId: FormControl;
  public selectedBulletinsIds: FormControl;

  constructor() {
    super();
    this.firstSignerId = new FormControl(null);
    this.secondSignerId = new FormControl(null);
    this.validFrom = new FormControl(null);
    this.validTo = new FormControl(null);
    this.applicationId = new FormControl(null);
    this.statusCode = new FormControl(null);
    this.statusName = new FormControl(null);
    this.docName = new FormControl(null);
    this.registrationNumber = new FormControl(null);
    this.currentUserAuthId = new FormControl(null);
    this.selectedBulletinsIds = new FormControl(null);

    //Validators
    this.registrationNumber.disable();
    this.statusName.disable();
    this.docName.disable();

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      firstSignerId: this.firstSignerId,
      docName: this.docName,
      secondSignerId: this.secondSignerId,
      validFrom: this.validFrom,
      validTo: this.validTo,
      applicationId: this.applicationId,
      statusCode: this.statusCode,
      statusName: this.statusName,
      registrationNumber: this.registrationNumber,
      currentUserAuthId: this.currentUserAuthId,
      selectedBulletinsIds: this.selectedBulletinsIds,
    });
  }
}
