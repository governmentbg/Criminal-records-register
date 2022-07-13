import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { BaseForm } from "../../../../@core/models/common/base.form";
import { createEgnValidator } from "../../../../@core/validators/egn-validation-function";
import { createEmailValidator } from "../../../../@core/validators/email-validator-function";

export class UsersExternalForm extends BaseForm {
  public group: FormGroup;

  public name: FormControl;
  public active: FormControl;
  public isAdmin: FormControl;
  public email: FormControl;
  public egn: FormControl;
  public position: FormControl;
  public administrationId: FormControl;

  constructor() {
    super();
    this.name = new FormControl(null, [Validators.required]);
    this.active = new FormControl(null);
    this.isAdmin = new FormControl(null);
    this.email = new FormControl(null, [createEmailValidator()]);
    this.egn = new FormControl(null, [Validators.required, createEgnValidator()]);
    this.position = new FormControl(null);
    this.administrationId = new FormControl(null, [Validators.required]);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      name: this.name,
      active: this.active,
      isAdmin: this.isAdmin,
      email: this.email,
      egn: this.egn,
      position: this.position,
      administrationId: this.administrationId,
    });
  }
}
