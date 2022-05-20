import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class AdministrationsExtForm extends BaseForm {
  public group: FormGroup;

  public name: FormControl;
  public descr: FormControl;

  constructor() {
    super();
    this.name = new FormControl(null, [Validators.required]);
    this.descr = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      name: this.name,
      descr: this.descr,
    });
  }
}
