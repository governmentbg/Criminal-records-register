import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../../../../@core/models/common/base.form";

export class SearchPersonForm extends BaseForm {
  public group: FormGroup;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public sex: FormControl;
  public birthDate: FormControl;

  constructor() {
    super();
    this.firstname = new FormControl(null, [Validators.required]);
    this.surname = new FormControl(null, [Validators.required]);
    this.familyname = new FormControl(null, [Validators.required]);
    this.sex = new FormControl(null, [Validators.required]);
    this.birthDate = new FormControl(null, [Validators.required]);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      sex: this.sex,
      birthDate: this.birthDate,
    });
  }
}
