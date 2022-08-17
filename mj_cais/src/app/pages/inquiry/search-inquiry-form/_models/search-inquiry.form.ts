import { FormControl, FormGroup } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class SearchInquiryForm extends BaseForm {
  public group: FormGroup;

  public registrationNumber: FormControl;
  public statusCodeDisplayValue: FormControl;
  public validFrom: FormControl;
  public validTo: FormControl;
  public repApplRegistrationNumber: FormControl;
  public applicantData: FormControl;
  public personIdentificators: FormControl;
  public names: FormControl;
  public purpose: FormControl;
  public firstSigner: FormControl;
  public secondSigner: FormControl;

  constructor() {
    super();
    this.registrationNumber = new FormControl(null);
    this.statusCodeDisplayValue = new FormControl(null);
    this.validFrom = new FormControl(null);
    this.validTo = new FormControl(null);
    this.repApplRegistrationNumber = new FormControl(null);
    this.applicantData = new FormControl(null);
    this.personIdentificators = new FormControl(null);
    this.names = new FormControl(null);
    this.purpose = new FormControl(null);
    this.firstSigner = new FormControl(null);
    this.secondSigner = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      registrationNumber: this.registrationNumber,
      statusCodeDisplayValue: this.statusCodeDisplayValue,
      validFrom: this.validFrom,
      validTo: this.validTo,
      repApplRegistrationNumber: this.repApplRegistrationNumber,
      applicantData: this.applicantData,
      personIdentificators: this.personIdentificators,
      names: this.names,
      purpose: this.purpose,
      firstSigner: this.firstSigner,
      secondSigner: this.secondSigner,
    });
  }
}
