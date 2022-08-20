import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class AdministrationsExtForm extends BaseForm {
  public group: FormGroup;

  public name: FormControl;
  public descr: FormControl;
  public role: FormControl;
  public extAdministrationUics : FormControl;

  constructor() {
    super();
    this.name = new FormControl(null, [Validators.required]);
    this.descr = new FormControl(null);
    this.role = new FormControl(null);
    this.extAdministrationUics  = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      name: this.name,
      descr: this.descr,
      role: this.role,
      extAdministrationUics: this.extAdministrationUics 
    });
  }
}
