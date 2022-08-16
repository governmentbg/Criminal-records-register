import { FormControl, FormGroup } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class ApplicatonSearchForm extends BaseForm {
  public group: FormGroup;
  public certificateRegistrationNumber: FormControl;
  public statusCode: FormControl;
  public statusCodeDisplayValue: FormControl;
  public validFrom: FormControl;
  public validTo: FormControl;
  public registrationNumber: FormControl;
  public personIdentificator: FormControl;
  public names: FormControl;
  public firstSigner: FormControl;
  public secondSigner: FormControl;
  public accessCode: FormControl;

  constructor() {
    super();
    this.certificateRegistrationNumber = new FormControl(null);
    this.statusCode = new FormControl(null);
    this.statusCodeDisplayValue = new FormControl(null);
    this.validFrom = new FormControl(null);
    this.validTo = new FormControl(null);
    this.registrationNumber = new FormControl(null);
    this.personIdentificator = new FormControl(null);
    this.names = new FormControl(null);
    this.firstSigner = new FormControl(null);
    this.secondSigner = new FormControl(null);
    this.accessCode = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      certificateRegistrationNumber: this.certificateRegistrationNumber,
      statusCode: this.statusCode,
      statusCodeDisplayValue: this.statusCodeDisplayValue,
      validFrom: this.validFrom,
      validTo: this.validTo,
      registrationNumber: this.registrationNumber,
      personIdentificator: this.personIdentificator,
      names: this.names,
      firstSigner: this.firstSigner,
      secondSigner: this.secondSigner,
      accessCode: this.accessCode,
    });
  }
}
