import { FormControl, FormGroup } from "@angular/forms";
import { DatePrecisionModelForm } from "../../../../@core/components/forms/inputs/date-precision/_models/date-precision.form";

export class PersonSearchForm {
  public group: FormGroup;
  public identifier: FormControl;
  public firstName: FormControl;
  public surName: FormControl;
  public familyName: FormControl;
  public fullName: FormControl;
  public birthDate: DatePrecisionModelForm;

  constructor() {
    this.identifier = new FormControl(null);
    this.firstName = new FormControl(null);
    this.surName = new FormControl(null);
    this.familyName = new FormControl(null);
    this.fullName = new FormControl(null);
    this.birthDate = new DatePrecisionModelForm();

    this.group = new FormGroup({
      identifier: this.identifier,
      firstName: this.firstName,
      surName: this.surName,
      familyName: this.familyName,
      fullName: this.fullName,
      birthDate: this.birthDate.group,
    });
  }
}
